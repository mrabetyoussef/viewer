using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly APS _aps;

    public AuthController(APS aps)
    {
        _aps = aps;
    }

    [HttpGet("token")]
public async Task<IActionResult> GetAccessToken()
{
    try
    {
        // Appel au service pour obtenir le jeton
        var token = await _aps.GetPublicToken();

        // Renvoyer une réponse réussie
        return Ok(new
        {
            access_token = token.AccessToken,
            expires_in = (long)Math.Round((token.ExpiresAt - DateTime.UtcNow).TotalSeconds)
        });
    }
    catch (Exception ex)
    {
        // Journaliser l'erreur pour le débogage
        Console.WriteLine($"Erreur dans GetAccessToken: {ex.Message}");

        // Renvoyer une réponse d'erreur HTTP 500
        return StatusCode(500, new
        {
            error = "An error occurred while generating the access token.",
            details = ex.Message // Vous pouvez supprimer ce détail en production pour des raisons de sécurité
        });
    }
}

}