namespace AntroStop.Domain.Base.AuthModels
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Token { get; set; }
    }
}
