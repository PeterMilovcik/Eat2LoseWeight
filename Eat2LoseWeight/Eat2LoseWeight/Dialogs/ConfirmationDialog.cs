using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight.Dialogs
{
    public interface IConfirmationDialog
    {
        Task<bool> ShowDialog();
    }

    public class ConfirmationDialog : IConfirmationDialog
    {
        private Page Page { get; }

        public ConfirmationDialog(Page page)
        {
            Page = page;
        }

        public async Task<bool> ShowDialog() =>
            await Page.DisplayAlert(
                "Confirmation",
                "Are you sure?",
                "Yes",
                "No");
    }
}