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

        public ActionTypes LoadAction { get; private set; }
        public string Errors { get; private set; }

        public async Task LoadAsync(Func<Task> func, bool isToaster = false)
        {
            try
            {
                LoadAction = ActionTypes.InProgress;
                StateHasChanged();

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
                    MatToaster.Add(ex.Message, MatToastType.Danger);
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
                MatToaster.Add(ex.Message, MatToastType.Danger);
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}