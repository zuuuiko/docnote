using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace docnote.Resources
{
    class SelectAllTextBoxBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.GotFocus += this.OnTextBoxGotFocus;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            // Recommended best practice: 
            // Detach the registered event handler to avoid memory leaks.
            this.AssociatedObject.GotFocus -= this.OnTextBoxGotFocus;
            base.OnDetaching();
        }

        /// <summary>
        /// Handles the GotFocus event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            this.AssociatedObject.SelectAll();
        }
    }
}
