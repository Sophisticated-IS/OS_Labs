using System;
using System.Windows.Controls;
using System.Windows.Documents;

namespace EncryptionMessage_6_7.Extensions
{
    public static class RichTextBoxExtension
    {
        public static string GetText(this RichTextBox richTextBox)
        {
            if (richTextBox == null) throw new ArgumentNullException(nameof(richTextBox));

            var textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                richTextBox.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                richTextBox.Document.ContentEnd);
            
            return textRange.Text;
        }

      
    }
}