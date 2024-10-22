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
                ShowTipUtil.ShowTip("������ѡ��һ���ַ����ͣ�", uiPanel4);
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
                ShowTipUtil.ShowTip("��������Ѹ��Ƶ������塣", uiPanel4);
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

            // ���ڳ�����������
            if (password.Length >= 8) strength++;
            if (password.Length >= 12) strength++;
            if (password.Length >= 16) strength++;

            // ���� GetDiversityScore ������ȡ�ַ�����������
            int diversity = GetDiversityScore(password);

            // ����ַ����͵Ķ������㹻��������ǿ������
            if (diversity >= 2)
                strength += diversity;

            // ���볤�ȹ��̻������������
            if (password.Length <= 6)
                strength = Math.Min(strength, 2);

            // ��֤��Сǿ��
            if (strength < 1)
                strength = 1;

            // ����ǿ�����Ϊ 5
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
                1 => "����",
                2 => "����",
                3 => "�е�",
                4 => "��ǿ",
                5 => "��ǿ",
                _ => "����ǿ��",
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
