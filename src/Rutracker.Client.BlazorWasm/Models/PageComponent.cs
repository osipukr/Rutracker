using System;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace Rutracker.Client.BlazorWasm.Models
{
    public class PageComponent : ComponentBase
    {
        [Inject]
        public IMatToaster MatToaster { get; set; }

        public ActionTypes LoadAction { get; set; } = ActionTypes.InProgress;
        public string Errors { get; set; }

        public async Task LoadAsync(Func<Task> func, bool isToaster = false, string toasterTitle = null)
        {
            try
            {
                await func();

                LoadAction = ActionTypes.Succeeded;
                Errors = null;
            }
            catch (Exception ex)
            {
                LoadAction = ActionTypes.Failed;
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

        public async Task ActionAsync(Func<Task> func, string successMessage = null)
        {
            try
            {
                await func();

                if (!string.IsNullOrWhiteSpace(successMessage))
                {
                    MatToaster.Add(successMessage, MatToastType.Success);
                }
            }
            catch (Exception ex)
            {
                MatToaster.Add(ex.Message, MatToastType.Warning);
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}