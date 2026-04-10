using ExcelDna.Integration.CustomUI;
using System.Runtime.InteropServices;

namespace Ezcel.AddIn.Ribbon
{
    [ComVisible(true)]
    public class Ribbon : ExcelRibbon
    {
        public override string GetCustomUI(string RibbonID)
        {
            return @"
            <customUI xmlns='http://schemas.microsoft.com/office/2009/07/customui'>
                <ribbon>
                    <tabs>
                        <tab id='EzcelTab' label='Ezcel AI'>
                            <group id='EzcelGroup' label='AI 配置'>
                                <button id='SettingsButton' label='模型配置' size='large' 
                                    imageMso='FileOptions' onAction='OnSettings' />
                                <button id='AboutButton' label='关于' 
                                    imageMso='HelpAbout' onAction='OnAbout' />
                            </group>
                        </tab>
                    </tabs>
                </ribbon>
            </customUI>";
        }

        public void OnSettings(IRibbonControl control)
        {
            // 这里将打开模型配置面板
        }

        public void OnAbout(IRibbonControl control)
        {
            // 这里将显示关于对话框
        }
    }
}