namespace Passarinho;

public partial class MainPage : ContentPage
{
	const int ForcaPulo = 30;
	const int MaxTempoPulando = 3;
	const int Gravidade = 5;
	const int TempoEntreFrames = 25;//ms
	const int aberturaMinima = 100;
	int Velocidade = 20;
	int tempoPulando = 0;
	bool EstaMorto = true;
	bool EstaPulando = false;
	double LarguraJanela = 0;
	double AlturaJanela = 0;

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
		imgcactobaixo.TranslationX = 0;
		imgcactocima.TranslationX = 0;
	}
	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		LarguraJanela = w;
		AlturaJanela = h;
	}
	void GerenciaCanos()
	{
		imgcactocima.TranslationX -= Velocidade;
		imgcactobaixo.TranslationX -= Velocidade;
		if (imgcactobaixo.TranslationX < -LarguraJanela)
		{
			imgcactobaixo.TranslationX = 0;
			imgcactocima.TranslationX = 0;
		var alturaMaxima = -100;
		var alturaMinima = -imgcactobaixo.HeightRequest;

		imgcactocima.TranslationY = Random.Shared.Next((int)alturaMinima, (int)alturaMaxima);
		imgcactobaixo.TranslationY = imgcactocima.TranslationY + aberturaMinima + imgcactobaixo.HeightRequest;
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
		if (!EstaMorto)
		{
			if (VerificaColizaoTeto() || VerificaColizaoChao())
			{
				return true;
			}
		}

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
}

