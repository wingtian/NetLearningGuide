namespace NetLearningGuide.Core.ConfigurationSetting
{
    public class ConfigurationSetting<T> : IConfigurationSetting
    {
        public virtual T Value { get; set; }

        public static implicit operator T(ConfigurationSetting<T> setting)
        {
            return setting.Value;
        }
    }
}
