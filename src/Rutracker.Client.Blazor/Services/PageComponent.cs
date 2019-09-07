using System;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace Rutracker.Client.Blazor.Services
{
    public class PageComponent : ComponentBase
    {
        [Inject]
        public IMatToaster MatToaster { get; set; }

        public ServiceResult Result { get; set; }
        public string Errors { get; set; }

        public async Task LoadAsync(Func<Task> loadFunc, bool isUsedToaster = false, string toasterTitle = null)
        {
            try
            {
                Result = ServiceResult.InProgress;
                StateHasChanged();

                await loadFunc();

                Result = ServiceResult.Succeeded;
                Errors = null;
            }
            catch (Exception ex)
            {
                Result = ServiceResult.Failed;
                Errors = ex.Message;

                if (isUsedToaster)
                {
                    MatToaster.Add(ex.Message, MatToastType.Warning, toasterTitle);
                }
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}