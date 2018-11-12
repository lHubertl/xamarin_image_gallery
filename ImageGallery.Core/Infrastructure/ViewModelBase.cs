using System;
using System.Threading.Tasks;
using ImageGallery.Core.BusinessLogic.Responses;
using ImageGallery.Core.Resources;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ImageGallery.Core.Infrastructure
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; }
        protected IPageDialogService DialogService { get; }

        public ViewModelBase(
            INavigationService navigationService,
            IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        #region Data layer method

        /// <summary>
        /// The method that performs the task and processes the result of response
        /// </summary>
        /// <typeparam name="T">Type of request</typeparam>
        /// <param name="request">The task will be performed and processed</param>
        /// <param name="allowRetry">This parameter will allow retry action</param>
        /// <returns>Response data</returns>
        protected virtual async Task<T> PerformDataRequestAsync<T>(Func<Task<IResponseData<T>>> request, bool allowRetry = true)
        {
            var response = await request.Invoke();
            if (response.IsSuccess)
            {
                return response.Data;
            }

            // If response is negative then notify user
            if (allowRetry)
            {
                var retry = await UserNotificationAsync(
                    response.ErrorMessage,
                    ToMessage(response.Code),
                    Strings.U_Cancel,
                    Strings.U_Retry);

                if (retry)
                {
                    // If user expect retry then call recursively
                    return await PerformDataRequestAsync(request);
                }
            }
            else
            {
                await UserNotificationAsync(
                    response.ErrorMessage,
                    ToMessage(response.Code),
                    Strings.U_Cancel);
            }

            return default(T);
        }

        /// <summary>
        /// The method that performs the task and processes the result of response
        /// </summary>
        /// <param name="request">The task will be performed and processed</param>
        /// <param name="allowRetry">This parameter will allow retry action</param>
        /// <returns>Is response succeed</returns>
        protected virtual async Task<bool> PerformDataRequestAsync(Func<Task<IResponse>> request, bool allowRetry = true)
        {
            var response = await request.Invoke();
            if (response.IsSuccess)
            {
                return response.IsSuccess;
            }

            // If response is negative then notify user
            if (allowRetry)
            {
                var retry = await UserNotificationAsync(
                    response.ErrorMessage,
                    ToMessage(response.Code),
                    Strings.U_Cancel,
                    Strings.U_Retry);

                if (retry)
                {
                    // If user expect retry then call recursively
                    return await PerformDataRequestAsync(request);
                }
            }
            else
            {
                await UserNotificationAsync(
                    response.ErrorMessage,
                    ToMessage(response.Code),
                    Strings.U_Cancel);
            }

            return false;
        }

        #endregion

        /// <summary>
        /// This method will notify user about exeptions, this can be used as task repeater
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="title">Title</param>
        /// <param name="cancel">Cancel</param>
        /// <param name="accept">Accept - can be user like Retry command</param>
        /// <returns>If true then accept was pressed</returns>
        protected async Task<bool> UserNotificationAsync(string message, string title = null, string cancel = null, string accept = null)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (cancel == null)
                {
                    cancel = Strings.U_Cancel;
                }

                if (accept == null)
                {
                    await DialogService.DisplayAlertAsync(title, message, cancel);
                    taskCompletionSource.SetResult(false);
                }
                else
                {
                    bool alertResult = await DialogService.DisplayAlertAsync(title, message, accept, cancel);
                    taskCompletionSource.SetResult(alertResult);
                }
            });

            return await taskCompletionSource.Task;
        }

        private string ToMessage(ResponseCode code)
        {
            string codeMessage;

            switch (code)
            {
                case ResponseCode.JsonFail:
                case ResponseCode.ServerError:
                    codeMessage = Strings.E_ServerError;
                    break;

                case ResponseCode.InvalidCredentials:
                    codeMessage = string.Empty;
                    break;

                default:
                    codeMessage = Strings.Error;
                    break;
            }

            return codeMessage;
        }

    }
}
