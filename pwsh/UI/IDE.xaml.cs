using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace pwsh.UI
{
	/// <summary>
	/// Interaction logic for IDE.xaml
	/// </summary>
	public partial class IDE : UserControl
	{
		private Dictionary<string, Color> dict;
		public DB.uipwsh_cmdlet Cmdlet;

		public IDE()
		{
			InitializeComponent();
			// Define Color Dictionary for Syntax Highlighting.
			// Todo: Move this into a seperate File to load up when detect language. Like PS1 ETC
			dict = new Dictionary<string, Color>
			{
				// Dictionary Uses URI http://atelierbram.github.io/syntax-highlighting/atelier-schemes/cave/
				{ @"return", ColorFromHex("#955ae7") },
				{ @"\$([\w+\d+\-]*)", ColorFromHex("#398bc6")},
				{ @"function", ColorFromHex("#398bc6")},

				{ @"\$", ColorFromHex("#576ddb")},
				{ @"\[", ColorFromHex("#398bc6")},
				{ @"\]", ColorFromHex("#398bc6")},
				{ @"\(", ColorFromHex("#576ddb")},
				{ @"\)", ColorFromHex("#576ddb")},
				{ "\"[^\"]*\"", ColorFromHex("#a06e3b") },
				{ @"(param)|(foreach)|( for )|(while)|( if )|(else)|(elseif)|(try)|(catch)|(continue)|(break)", ColorFromHex("#bf40bf") },
				{ @"(#[\b\w\d\W]*)", ColorFromHex("#2a9292") },
				{ @"(<#[\S\b\w\d\W]*#>)", ColorFromHex("#2a9292") },
			};
		}
		private Color ColorFromHex(string hex)
		{
			Color col = (Color)ColorConverter.ConvertFromString(hex);
			return col;
		}
		public async Task UpdateDocument() {
			List<Paragraph> paragraphs = new List<Paragraph>();
			foreach (Paragraph p in Editor.Document.Blocks)
			{
				paragraphs.Add(p);
			}
			foreach (Paragraph p in paragraphs)
				UpdateParagraph(p);
			UpdateLayout();
		}
		public async Task UpdateLayout()
		{
			Editor.UpdateLayout();
		}
		public async Task UpdateParagraph() {
			/// Updates Current Paragraph;
			UpdateParagraph(Editor.Selection.Start.Paragraph);
		}
		private async Task UpdateParagraph(Paragraph p) {
			if (p == null) {
				return;
			}
			/// Updates a Specific Paragraph
			List<Tuple<TextPointer, TextPointer, Color>> Colorizer = new List<Tuple<TextPointer, TextPointer, Color>>();

			TextRange pText = new TextRange(p.ContentStart, p.ContentEnd);
			pText.ClearAllProperties();

			foreach (KeyValuePair<string, Color> obj in dict) {
				foreach (Match i in Regex.Matches(pText.Text, obj.Key)) {
					Colorizer.Add(new Tuple<TextPointer, TextPointer, Color>(p.ContentStart.GetPositionAtOffset(i.Index + 1), p.ContentStart.GetPositionAtOffset(i.Index + i.Length + 1), obj.Value));
				}
			}
			foreach (Tuple<TextPointer, TextPointer, Color> i in Colorizer)
			{
				TextRange pTextC = new TextRange(i.Item1, i.Item2);
				
				pTextC.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(i.Item3));
			}
			Editor.UpdateLayout();
		}
		public void AppendText(string v) {
			Editor.AppendText(v);
		}
		public void SetCmdlet(DB.uipwsh_cmdlet cmdlet) {
			Cmdlet = cmdlet;
			Clear();
			AppendText(Cmdlet.SCRIPT);
			//MessageBox.Show(Cmdlet.SCRIPT);
			UpdateDocument();
		}
		public DB.uipwsh_cmdlet GetCmdlet() {
			return Cmdlet;
		}
		public void ClearCmdlet() {
			Cmdlet = null;
			Clear();
		}
		public void Clear()
		{
			TextRange textRange = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
			textRange.Text = "";
		}
		public void InsertText(string text)
		{
			TextRange textRange = new TextRange(Editor.Selection.Start, Editor.Selection.End);
			textRange.Text = text;
			Editor.Selection.Select(Editor.Selection.End, Editor.Selection.End);
		}
		private async void Editor_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Tab) {
				// Override TAB Default Functionality.
				new TextRange(Editor.Selection.Start, Editor.Selection.End).Text = "\t";
				Editor.Selection.Select(Editor.Selection.End, Editor.Selection.End);
				e.Handled = true;
				return;
			}
			if ((e.Key == Key.Enter) & (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))) {
				// Blocks the Addition of a LineItem when you add a new line using Shift+Enter
				e.Handled = true;
				return;
			}
			if ((e.Key == Key.V) & (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))) {
				// Adds only text when you press CTRL+V
				InsertText(Clipboard.GetText());
				UpdateDocument();
				e.Handled = true;
				return;
			}
			if ((e.Key == Key.S) & (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))) {
				if (Cmdlet != null) {
					TextRange content = new TextRange(Editor.Document.ContentStart, Editor.Document.ContentEnd);
					Cmdlet.SCRIPT = content.Text;
					Constants.DB_table_pwsh_cmdlet.Save(Cmdlet.GetObject());
					Constants.Actionbar_Explorer_Cmdlets.SetDataContext( Constants.Actionbar_Explorer_Cmdlets.Console );
				}
			}
			UpdateParagraph();
		}
	}
}
