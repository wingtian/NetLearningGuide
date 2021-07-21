using DotnetSpider;
using DotnetSpider.DataFlow;
using DotnetSpider.DataFlow.Parser;
using DotnetSpider.DataFlow.Parser.Formatters;
using DotnetSpider.DataFlow.Storage;
using DotnetSpider.Downloader;
using DotnetSpider.Http;
using DotnetSpider.Infrastructure;
using DotnetSpider.Scheduler;
using DotnetSpider.Scheduler.Component;
using DotnetSpider.Selector;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using DotnetSpider.MySql.Scheduler;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace NetLearningGuide.IntegrationTests.Demo
{
    public class DotnetSpiderTest : TestBase
    {
        #region GithubSpiderTest
        [Fact]
        public async Task GithubSpiderTest()
        {
            ThreadPool.SetMaxThreads(255, 255);
            ThreadPool.SetMinThreads(255, 255);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console().WriteTo.RollingFile("logs/spider.log")
                .CreateLogger();

            var builder = Builder.CreateDefaultBuilder<GithubSpider>(options =>
            {
                // 每秒 1 个请求
                options.Speed = 1;
            });
            builder.UseSerilog();
            builder.UseQueueDistinctBfsScheduler<HashSetDuplicateRemover>();
            await builder.Build().RunAsync();


        }
        public class GithubSpider : Spider
        {
            public GithubSpider(IOptions<SpiderOptions> options, DependenceServices services, ILogger<Spider> logger) :
                base(options, services, logger)
            {
            }

            protected override async Task InitializeAsync(CancellationToken stoppingToken)
            {
                // 添加自定义解析
                AddDataFlow(new Parser());
                // 使用控制台存储器
                AddDataFlow(new ConsoleStorage());
                // 添加采集请求
                await AddRequestsAsync(new Request("https://github.com/zlzforever")
                {
                    // 请求超时 10 秒
                    Timeout = 10000
                });
            }

            protected override SpiderId GenerateSpiderId()
            {
                return new(ObjectId.CreateId().ToString(), "Github");
            }

            class Parser : DataParser
            {
                public override Task InitializeAsync()
                {
                    return Task.CompletedTask;
                }

                protected override Task ParseAsync(DataFlowContext context)
                {
                    var selectable = context.Selectable;
                    // 解析数据
                    var author = selectable.XPath("//span[@class='p-name vcard-fullname d-block overflow-hidden']")
                        ?.Value;
                    var name = selectable.XPath("//span[@class='p-nickname vcard-username d-block']")
                        ?.Value;
                    context.AddData("author", author);
                    context.AddData("username", name);
                    return Task.CompletedTask;
                }
            }
        }
        #endregion
        //TODO :WIP
        #region cnblogs by mysql

        [Fact]
        public async Task CnBlogspiderTest()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console().WriteTo.RollingFile("logs/spiders.log")
                .CreateLogger();

            var builder = Builder.CreateDefaultBuilder<EntitySpider>(options =>
            {
                options.Speed = 1;
            });
            builder.UseDownloader<HttpClientDownloader>();
            builder.UseSerilog();
            builder.IgnoreServerCertificateError();
            builder.UseMySqlQueueBfsScheduler(x =>
            {
                x.ConnectionString = Configuration.GetValue<string>("ConnectionStrings:Mysql");
            });
            await builder.Build().RunAsync();
            Environment.Exit(0);
        }
        public class EntitySpider : Spider
        {
            public EntitySpider(IOptions<SpiderOptions> options, DependenceServices services,
                ILogger<Spider> logger) : base(
                options, services, logger)
            {
            }

            protected override async Task InitializeAsync(CancellationToken stoppingToken = default)
            {
                AddDataFlow(new DataParser<CnblogsEntry>());
                AddDataFlow(GetDefaultStorage());
                await AddRequestsAsync(
                    new Request(
                        "https://news.cnblogs.com/n/page/1", new Dictionary<string, object> { { "网站", "博客园" } }));
            }

            protected override SpiderId GenerateSpiderId()
            {
                return new(ObjectId.CreateId().ToString(), "博客园");
            }

            [Schema("cnblogs", "news")]
            [EntitySelector(Expression = ".//div[@class='news_block']", Type = SelectorType.XPath)]
            [GlobalValueSelector(Expression = ".//a[@class='current']", Name = "类别", Type = SelectorType.XPath)]
            [GlobalValueSelector(Expression = "//title", Name = "Title", Type = SelectorType.XPath)]
            [FollowRequestSelector(Expressions = new[] { "//div[@class='pager']" })]
            public class CnblogsEntry : EntityBase<CnblogsEntry>
            {
                protected override void Configure()
                {
                    HasIndex(x => x.Title);
                    HasIndex(x => new { x.WebSite, x.Guid }, true);
                }

                public int Id { get; set; }

                [Required]
                [StringLength(200)]
                [ValueSelector(Expression = "类别", Type = SelectorType.Environment)]
                public string Category { get; set; }

                [Required]
                [StringLength(200)]
                [ValueSelector(Expression = "网站", Type = SelectorType.Environment)]
                public string WebSite { get; set; }

                [StringLength(200)]
                [ValueSelector(Expression = "Title", Type = SelectorType.Environment)]
                [ReplaceFormatter(NewValue = "", OldValue = " - 博客园")]
                public string Title { get; set; }

                [StringLength(40)]
                [ValueSelector(Expression = "GUID", Type = SelectorType.Environment)]
                public string Guid { get; set; }

                [ValueSelector(Expression = ".//h2[@class='news_entry']/a")]
                public string News { get; set; }

                [ValueSelector(Expression = ".//h2[@class='news_entry']/a/@href")]
                public string Url { get; set; }

                [ValueSelector(Expression = ".//div[@class='entry_summary']")]
                [TrimFormatter]
                public string PlainText { get; set; }

                [ValueSelector(Expression = "DATETIME", Type = SelectorType.Environment)]
                public DateTime CreationTime { get; set; }
            }
        }



        #endregion
    }
}
