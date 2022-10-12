using Chess.Chess_Piece;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class PawnPromoteForm : Form
    {
        public string Selection { get { return (string) promoteComboBox.SelectedItem; } }
        public PawnPromoteForm(EventHandler onClick)
        {
            InitializeComponent();
            promoteComboBox.SelectedIndex = 0;
            submitPromotionButton.Click += onClick;
        }

        private void submitPromotionButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
