using KCNPasswordGenerator.Properties;
using KCNPasswordGenerator.Utils;

namespace KCNPasswordGenerator
{
    public partial class FormMain : Sunny.UI.UIForm
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.icon;
            this.ShowIcon = true;
            uiTrackBar1.Value = 15;
            uiCheckBox1.Checked = uiCheckBox2.Checked = uiCheckBox3.Checked = true;
            SetStrengthPanel(0);
        }

        private void uiButton7_Click(object sender, EventArgs e)
        {
            if (uiCheckBox1.Checked == false && uiCheckBox2.Checked == false
                && uiCheckBox3.Checked == false && uiCheckBox4.Checked == false)
            {
                ShowTipUtil.ShowTip("请至少选择一项字符类型！", uiPanel4);
                return;
            }

            string pass = RandomUtil.GetRandomizer(uiTrackBar1.Value, uiCheckBox3.Checked, uiCheckBox4.Checked, uiCheckBox2.Checked, uiCheckBox1.Checked);
            uiTextBox1.Text = pass;
            SetStrengthPanel(CalculatePasswordStrength(pass));
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(uiTextBox1.Text))
            {
                Clipboard.SetText(uiTextBox1.Text);
                ShowTipUtil.ShowTip("随机密码已复制到剪贴板。", uiPanel4);
            }
        }

        private void SetStrengthPanel(int value)
        {
            uiPanel5.Text = PasswordStrengthText(value);
            uiPanel5.FillColor = PasswordStrengthColor(value);
        }

        private int CalculatePasswordStrength(string password)
        {
            int strength = 0;

            // 基于长度增加评分
            if (password.Length >= 8) strength++;
            if (password.Length >= 12) strength++;
            if (password.Length >= 16) strength++;

            // 调用 GetDiversityScore 方法获取字符多样性评分
            int diversity = GetDiversityScore(password);

            // 如果字符类型的多样性足够，则增加强度评分
            if (diversity >= 2)
                strength += diversity;

            // 密码长度过短会限制最大评分
            if (password.Length <= 6)
                strength = Math.Min(strength, 2);

            // 保证最小强度
            if (strength < 1)
                strength = 1;

            // 限制强度最高为 5
            return Math.Min(strength, 5);
        }

        private int GetDiversityScore(string password)
        {
            int diversity = 0;
            diversity += password.Any(char.IsUpper) ? 1 : 0;
            diversity += password.Any(char.IsLower) ? 1 : 0;
            diversity += password.Any(char.IsDigit) ? 1 : 0;
            diversity += password.Any(IsSpecialCharacter) ? 1 : 0;
            return diversity;
        }

        private bool IsSpecialCharacter(char c)
        {
            int ascii = c;
            return (ascii >= 33 && ascii <= 47) ||
                   (ascii >= 58 && ascii <= 64) ||
                   (ascii >= 91 && ascii <= 96) ||
                   (ascii >= 123 && ascii <= 126);
        }

        private string PasswordStrengthText(int value)
        {
            return value switch
            {
                1 => "很弱",
                2 => "较弱",
                3 => "中等",
                4 => "较强",
                5 => "很强",
                _ => "密码强度",
            };
        }

        private Color PasswordStrengthColor(int value)
        {
            return value switch
            {
                1 => Color.Red,
                2 => Color.Orange,
                3 => Color.Goldenrod,
                4 => Color.LimeGreen,
                5 => Color.Green,
                _ => Color.Gray,
            };
        }

        private void uiTrackBar1_ValueChanged(object sender, EventArgs e) => uiLabel3.Text = uiTrackBar1.Value.ToString();

        private void UIicon_Click(object sender, EventArgs e) => uiTrackBar1.Value = uiTrackBar1.Value - 1;

        private void uiSymbolButton1_Click(object sender, EventArgs e) => uiTrackBar1.Value = uiTrackBar1.Value + 1;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }
}
