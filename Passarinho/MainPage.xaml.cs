namespace Passarinho;

public partial class MainPage : ContentPage
{
	double LarguraJanela = 0;
	double AlturaJanela = 0;
	int Velocidade = 20;
	const int Gravidade = 1;
	const int TempoEntreFrames = 25;//ms
	bool EstaMorto = false;
	const int ForcaPulo = 30;
	const int MaxTempoPulando = 3;
	bool EstaPulando = false;
	int tempoPulando = 0;

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
			AplicaGravidade();
			if (VericaColizao())
			{
				EstaMorto = true;
				frameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(TempoEntreFrames);

		}
		if (EstaPulando)
			AplicaPulo();
		else
			AplicaGravidade();
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
		urubu.TranslationY = 0;
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
		var maxY = AlturaJanela / 2 - imgChao.HeightRequest - 210;

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
}

