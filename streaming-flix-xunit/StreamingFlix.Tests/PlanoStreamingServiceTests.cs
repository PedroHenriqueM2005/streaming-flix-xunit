using StreamingFlix.App;
using Xunit;

namespace StreamingFlix.Tests;

public class PlanoStreamingServiceTests
{
    private readonly PlanoStreamingService _planoStreamingService = new();

    [Theory]
    [InlineData(1, "BÁSICO")]    
    [InlineData(2, "PADRÃO")]    
    [InlineData(4, "PREMIUM")]   
    public void ObterClassificacaoPorQualidade_VariosCasos_RetornaClassificacaoCorreta(
        int telasSimultaneas, string classificacaoEsperada)
    {
        var resultado = _planoStreamingService.ObterClassificacaoPorQualidade(telasSimultaneas);

        Assert.Equal(classificacaoEsperada, resultado);
    }

    [Theory]
    [InlineData(50, 1, 50)]    
    [InlineData(50, 6, 45)]    
    [InlineData(50, 12, 40)]     
    public void CalcularMensalidadeComDesconto_VariosCasos_RetornaValorFinalCorreto(
        int valorBase, int mesesContratados, int mensalidadeEsperada)
    {
        var resultado = _planoStreamingService.CalcularMensalidadeComDesconto(valorBase, mesesContratados);

        Assert.Equal(mensalidadeEsperada, resultado);
    }

    [Theory]
    [InlineData(20, false, true)]   
    [InlineData(20, true, false)]   
    [InlineData(16, false, false)]  
    public void PodeAcessarConteudoAdulto_VariosCasos_RetornaElegibilidadeCorreta(
        int idade, bool controleParentalAtivo, bool acessoEsperado)
    {
        var resultado = _planoStreamingService.PodeAcessarConteudoAdulto(idade, controleParentalAtivo);

        Assert.Equal(acessoEsperado, resultado);
    }
}
