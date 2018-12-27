using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegexExpression
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnMatches_Click(object sender, EventArgs e)
        {
            WebClient web = new WebClient();
            var fonte = web.DownloadString("http://andresecco.com.br/2017/01/azure-tech-nights");

            var rgx = new Regex(@"Dia (\d) \((\d+\/\d+\/\d+)\)");

            var primeiroEncontrado = rgx.Match(fonte);
            var encontrados = rgx.Matches(fonte);

            Dictionary<int, DateTime> dic = new Dictionary<int, DateTime>();

            foreach (Match item in encontrados)
            {
                dic.Add(int.Parse(item.Groups[1].Value), DateTime.Parse(item.Groups[2].Value));
            }

            dic.ToList().ForEach(c => dataGridView1.Rows.Add(c.Key, c.Value));
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            var rgx = new Regex(@"http://.*\d{2}\/");

            var assuntosPost = rgx.Replace("http://andresecco.com.br/2017/01/azure-tech-nights/", string.Empty);
            MessageBox.Show(assuntosPost);
        }

        private void btnIsMatch_Click(object sender, EventArgs e)
        {
            MessageBox.Show("É conteúdo do azure?" + (PostDesseAno("http://andresecco.com.br/2017/01/azure-tech-nights/")));
        }

        private bool PostDesseAno(string url)
        {
            var rgx = new Regex(@"http://.*\/azure.*");

            return rgx.IsMatch(url);
        }
    }
}
