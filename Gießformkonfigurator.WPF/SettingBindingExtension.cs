using System.Windows.Data;

namespace Giessformkonfigurator.WPF
{
    public class SettingBindingExtension : Binding
    {
        public SettingBindingExtension()
        {
            Initialize();
        }

        public SettingBindingExtension(string path)
            : base(path)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.Source = Giessformkonfigurator.WPF.Properties.Settings.Default;
            this.Mode = BindingMode.TwoWay;
        }
    }
}
