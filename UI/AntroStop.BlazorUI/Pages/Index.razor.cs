using BlazorLeaflet;
using BlazorLeaflet.Models;
using System.Drawing;
using System.Threading.Tasks;
using AntroStop.Domain.Base.Models.Users;
using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.Components;

namespace AntroStop.BlazorUI.Pages
{
    public partial class Index
    {
        private int usersCount;
        private Map _map;


        [Inject]
        private IWebUsersRepository<UsersInfo> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            usersCount = await Users.Count();
        }

        protected override void OnInitialized()
        {
            _startAt = new PointF(Lat, Lng);

            _map = new Map(jsRuntime);

            _map.Layers.Add(new TileLayer
            {
                UrlTemplate = "https://a.tile.openstreetmap.org/{z}/{x}/{y}.png",
                Attribution = "&copy; <a href=\"https://www.openstreetmap.org/copyright\">OpenStreetMap</a> contributors",
            });

            var marker = new Marker(_startAt)
            {
                Draggable = true,
                Title = @"Marker 1"
            };


            _map.Layers.Add(marker);
        }

        private float Lat { get; set; } = 47.5574007f;

        private float Lng { get; set; } = 16.3918687f;

        private PointF _startAt;
    }
}
