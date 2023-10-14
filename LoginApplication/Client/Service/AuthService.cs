using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using LoginApplication.Client.Helper;
using LoginApplication.Shared;
using Microsoft.AspNetCore.Components.Authorization;

namespace LoginApplication.Client.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }


        public async Task<RegisteResult> Register(RegisterModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Accounts", registerModel);
            if (result.IsSuccessStatusCode)
                return new RegisteResult { Successful = true, Errors = new List<string> { "Cuenta creada satisfactoriamenteß" } };
            return new RegisteResult { Successful = false, Errors = new List<string> { "Ocurrió un error" } };
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            //// Convertir el modelo a Json
            //var loginAsJson = JsonSerializer.Serialize(loginModel);
            //// Agregar el contenido especificando el tipo json 
            //var response = await _httpClient.PostAsync("api/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            //// Leemos el contenido de respuesta y
            //// habilitamos la coincidencia de nombres de propiedades que no distinga entre mayúsculas y minúsculas 
            //var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(),
            //    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //if (!response.IsSuccessStatusCode)
            //{
            //    return loginResult!;
            //}
            //// guardamos el token en Local Storage
            //await _localStorage.SetItemAsync("authToken", loginResult!.Token);
            //// notificamos los cambios en el estado de autenticación
            //((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);
            //// agregamos el token al encabezado de las peticiones 
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            //return loginResult;

            var response = await _httpClient.PostAsJsonAsync("api/Login", loginModel);
            var responseContext = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (responseContext!.Successful)
            {
                await _localStorage.SetItemAsync("authToken", responseContext.Token);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(responseContext.Token!);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", responseContext.Token);
            }

            return responseContext;
        }

        public async Task Logout()
        {
            // Eliminamos el token del Local Storage
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}

