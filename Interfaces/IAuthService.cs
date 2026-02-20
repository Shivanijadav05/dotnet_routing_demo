using Microsoft.AspNetCore.Mvc;
using MyWebApi.DTOs;

public interface IAuthService{
    
     public Task<LoginResponseDTO> Login(LoginDTO model);
}