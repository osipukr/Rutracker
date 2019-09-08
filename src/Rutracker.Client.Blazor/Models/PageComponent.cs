using System;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace Rutracker.Client.Blazor.Models
{
    public class PageComponent : ComponentBase
    {
        [Inject]
        public IMatToaster MatToaster { get; set; }

        public PageActions ActionResult { get; set; } = PageActions.InProgress;
        public string Errors { get; set; } = string.Empty;

        public async Task LoadAsync(Func<Task> func, bool isToaster = false, string toasterTitle = null)
        {
            try
            {
                await func();

                ActionResult = PageActions.Succeeded;
                Errors = string.Empty;
            }
            catch (Exception ex)
            {
                ActionResult = PageActions.Failed;
                Errors = ex.Message;

                if (isToaster)
                {
                    MatToaster.Add(ex.Message, MatToastType.Warning, toasterTitle);
                }
            }
            finally
            {
                StateHasChanged();
            }
        }

        public async Task ActionAsync(Func<Task> func, string toasterTitle = null)
        {
            try
            {
                await func();
            }
            catch (Exception ex)
            {
                MatToaster.Add(ex.Message, MatToastType.Warning, toasterTitle);
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}