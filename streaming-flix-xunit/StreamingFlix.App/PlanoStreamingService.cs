namespace StreamingFlix.App;

public class PlanoStreamingService
{
    public string ObterClassificacaoPorQualidade(int telasSimultaneas)
    {
        if (telasSimultaneas >= 4)
        {
            return "PREMIUM";
        }

        if (telasSimultaneas >= 2)
        {
            return "PADRÃO";
        }

        return "BÁSICO";
    }

    public int CalcularMensalidadeComDesconto(int valorBase, int mesesContratados)
    {
        int percentualDesconto = 0;

        if (mesesContratados >= 12)
        {
            percentualDesconto = 20;
        }
        else if (mesesContratados >= 6)
        {
            percentualDesconto = 10;
        }

        return valorBase - (valorBase * percentualDesconto / 100);
    }

    public bool PodeAcessarConteudoAdulto(int idade, bool controleParentalAtivo)
    {
        return idade >= 18 && !controleParentalAtivo;
    }
}
