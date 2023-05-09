using AntroStop.Domain.Base.AuthModels;
using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.WebRepositories;
using AntroStop.MauiUI.Infrastructure.Extensions;
using Darnton.Blazor.DeviceInterop.Geolocation;
using Darnton.Blazor.Leaflet.LeafletMap;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AntroStop.MauiUI.Pages
{
    public partial class Index
    {
        //Блок авторизации пользователя
        private UserForAuthenticationDto _userForAuthentication = new UserForAuthenticationDto();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool ShowAuthError { get; set; }
        public string Error { get; set; }

        public async Task ExecuteLogin()
        {
            ShowAuthError = false;

            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;

                StateHasChanged();
            }
            else
            {
                NavigationManager.NavigateTo("/", true);
            }
        }


        //Создание заявки
        private ViolationsInfo violationsInfo;
        protected override void OnInitialized()
        {
            violationsInfo = new ViolationsInfo();
        }
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IWebViolationsRepository<ViolationsInfo> violationsRepository { get; set; }

        private string UserID = string.Empty;

        //Файлы
        public List<ElementsInfo> Elements = new List<ElementsInfo>();
        [Inject]
        public IWebElementsRepository<ElementsInfo> ElementsRepository { get; set; }


        //Карта
        [Inject] public IGeolocationService GeolocationService { get; set; }
        protected Darnton.Blazor.Leaflet.LeafletMap.Map PositionMap;
        protected TileLayer PositionTileLayer;
        protected Marker CurrentPositionMarker;
        protected GeolocationResult CurrentPositionResult { get; set; }
        protected string CurrentLatitude => CurrentPositionResult?.Position?.Coords?.Latitude.ToString("F2");
        protected string CurrentLongitude => CurrentPositionResult?.Position?.Coords?.Longitude.ToString("F2");
        protected bool ShowCurrentPositionError => CurrentPositionResult?.Error != null;
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public Index() : base()
        {
            PositionMap = new Darnton.Blazor.Leaflet.LeafletMap.Map("geolocationPointMap", new MapOptions //Centred on New Zealand
            {
                Center = new LatLng(60.965795, 94.772974),
                Zoom = 2
            });
            PositionTileLayer = new TileLayer(
                "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
                new TileLayerOptions
                {
                    Attribution = @"Map data &copy; <a href=""https://www.openstreetmap.org/"">OpenStreetMap</a> contributors, " +
                        @"<a href=""https://creativecommons.org/licenses/by-sa/2.0/"">CC-BY-SA</a>"
                }
            );

        }
        public async void ShowCurrentPosition()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (CurrentPositionMarker != null)
                {
                    await CurrentPositionMarker.Remove();
                }
                LatLng latLng = new LatLng(location.Latitude, location.Longitude);
                CurrentPositionMarker = new Marker(latLng, null);
                await CurrentPositionMarker.AddTo(PositionMap);

                violationsInfo.Coordinates = $"{location.Latitude};{location.Longitude}";
                StateHasChanged();
            }
            catch(Exception ex)
            {
                App.Current.MainPage.DisplayAlert(ex.Message,"Ошибка геолокации","OK");
            }
            finally { _isCheckingLocation = false; }
        }
        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }

        private async Task Save()
        {

            violationsInfo.Status = "Принята";
            violationsInfo.UserID = UserID;

            var violation = await violationsRepository.Add(violationsInfo);

            if (Elements.Count > 0)
            {
                foreach (var element in Elements)
                {
                    element.ViolationID = violation.Id;
                    element.Type = "File";
                    await ElementsRepository.Add(element);
                }
            }

            Navigation.NavigateTo($"/Violations/{UserID}");
        }
        private void AssignImageUrl(string imgUrl)
        {
            Elements.Add(new ElementsInfo { FileName = imgUrl });
        }
    }
}
