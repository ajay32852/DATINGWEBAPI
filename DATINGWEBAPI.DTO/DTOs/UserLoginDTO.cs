namespace DATINGWEBAPI.DTO.DTOs
{
    public class UserLoginDTO
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecretKey { get; set; } // JWT
        public UserDTO? UserData { get; set; }
    }

}
