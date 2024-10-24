namespace Passarinho;

public partial class MainPage : ContentPage
{
	const int ForcaPulo = 30;
	const int MaxTempoPulando = 3;
	const int Gravidade = 5;
	const int TempoEntreFrames = 25;//ms
	const int aberturaMinima = 200;
	int Velocidade = 20;
	int tempoPulando = 0;
	bool EstaMorto = true;
	bool EstaPulando = false;
	double LarguraJanela = 0;
	double AlturaJanela = 0;
	int pontuacao = 0;

	public MainPage()
	{
		InitializeComponent();
	}
	void AplicaGravidade()
	{
		urubu.TranslationY += Gravidade;
	}

	async Task Desenha()
	{
		while (!EstaMorto)
		{
			GerenciaCanos();
			if (EstaPulando)
				AplicaPulo();
			else
				AplicaGravidade();
			if (VericaColizao())
			{
				EstaMorto = true;
				frameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(TempoEntreFrames);
		}
	}

	void OnGridClicked(object s, TappedEventArgs a)
	{
		EstaPulando = true;
	}

	void OnGameOverClicked(object s, TappedEventArgs e)
	{
		frameGameOver.IsVisible = false;
		Inicializar();
		Desenha();
	}

	void Inicializar()
	{
		EstaMorto = false;
	urubu.TranslationY = 0;
		urubu.TranslationX = 0;
		imgcactobaixo.TranslationX = -LarguraJanela;
		imgcactocima.TranslationX = -LarguraJanela;
		pontuacao = 0;
		GerenciaCanos();
	}
	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		LarguraJanela = w;
		AlturaJanela = h;
		if (h > 0)
    {
      imgcactocima.HeightRequest  = h - imgChao.HeightRequest;
      imgcactobaixo.HeightRequest = h - imgChao.HeightRequest;
    }
	}
	void GerenciaCanos()
	{
		imgcactocima.TranslationX -= Velocidade;
		imgcactobaixo.TranslationX -= Velocidade;
		if (imgcactobaixo.TranslationX < -LarguraJanela)
		{
			imgcactobaixo.TranslationX = 0;
			imgcactocima.TranslationX = 0;
		var alturaMaxima = -(imgcactobaixo.HeightRequest * 0.1);
      var alturaMinima = -(imgcactobaixo.HeightRequest * 0.8);

		imgcactocima.TranslationY  = Random.Shared.Next((int)alturaMinima, (int)alturaMaxima);
      imgcactobaixo.TranslationY = imgcactocima.HeightRequest + imgcactocima.TranslationY + aberturaMinima;

      pontuacao++;
      labelPontuacao.Text = "Pontuação: " + pontuacao.ToString("D5");
      if (pontuacao % 4 == 0)
        Velocidade++;
		}
	}
	bool VerificaColizaoTeto()
	{
		var minY = -AlturaJanela / 2;

		if (urubu.TranslationY <= minY)
			return true;
		else
			return false;
	}

	bool VerificaColizaoChao()
	{
		var maxY = AlturaJanela / 2 - imgChao.HeightRequest;

		if (urubu.TranslationY >= maxY)
			return true;
		else
			return false;
	}

	bool VericaColizao()
	{
		return (!EstaMorto && (VerificaColizaoChao() || VerificaColizaoTeto() || VerificaColizaoCacto()));
	}
	bool VerificaColizaoCacto()
  {
    if (VerificaColizaoCactoBaixo() || VerificaColizaoCactoCima())
      return true;
    else
      return false;
  }


	void AplicaPulo()
	{
		urubu.TranslationY -= ForcaPulo;
		tempoPulando++;

		if (tempoPulando >= MaxTempoPulando)
		{
			EstaPulando = false;
			tempoPulando = 0;
		}
	}
	
	
	bool VerificaColizaoCactoCima()
	{
    var posHUrubu = (LarguraJanela - 50) - (urubu.WidthRequest / 2);
    var posVUrubu   = (AlturaJanela / 2) - (urubu.HeightRequest / 2) + urubu.TranslationY;

    if (
         posHUrubu >= Math.Abs(imgcactocima.TranslationX) - imgcactocima.WidthRequest &&
         posHUrubu <= Math.Abs(imgcactocima.TranslationX) + imgcactocima.WidthRequest &&
         posVUrubu   <= imgcactocima.HeightRequest + imgcactocima.TranslationY
       )
      return true;
    else
      return false;
  }

	bool VerificaColizaoCactoBaixo()
  {
    var posHUrubu = LarguraJanela - 50 - urubu.WidthRequest / 2;
    var posVUrubu   = (AlturaJanela / 2) + (urubu.HeightRequest / 2) + urubu.TranslationY;

    var yMaxCano = imgcactocima.HeightRequest + imgcactocima.TranslationY + aberturaMinima;

    if (
         posHUrubu >= Math.Abs(imgcactocima.TranslationX) - imgcactocima.WidthRequest &&
        posHUrubu <= Math.Abs(imgcactocima.TranslationX) + imgcactocima.WidthRequest &&
          posVUrubu   >= yMaxCano
       )
      return true;
    else
      return false;
  }
}

