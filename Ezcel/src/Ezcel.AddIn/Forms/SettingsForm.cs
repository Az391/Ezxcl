using System;using System.Windows.Forms;

namespace Ezcel.AddIn.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Ezcel AI 配置";
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            var tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // 模型配置选项卡
            var modelTab = new TabPage("模型配置");
            tabControl.TabPages.Add(modelTab);

            // 自定义提供者选项卡
            var customTab = new TabPage("自定义提供者");
            tabControl.TabPages.Add(customTab);

            // 全局设置选项卡
            var globalTab = new TabPage("全局设置");
            tabControl.TabPages.Add(globalTab);

            var okButton = new Button();
            okButton.Text = "确定";
            okButton.Dock = DockStyle.Bottom;
            okButton.Click += (sender, e) => this.Close();

            this.Controls.Add(tabControl);
            this.Controls.Add(okButton);
        }
    }
}