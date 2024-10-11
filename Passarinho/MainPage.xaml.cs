namespace Passarinho;

public partial class MainPage : ContentPage
{
	double LarguraJanela = 0;
	double AlturaJanela = 0;
	int Velocidade = 20;
	const int Gravidade = 1;
	const int TempoEntreFrames = 25;//ms
	bool EstaMorto = false;

	public MainPage()
	{
		InitializeComponent();
	}
    void AplicaGravidade()
	{
		urubu.TranslationY+=Gravidade; 
	}
	
	async Task Desenha()
	{
		while (!EstaMorto)
		{
			GerenciaCanos();
			AplicaGravidade();
			await Task.Delay(TempoEntreFrames);
		}
	}
	
			void Inicializar()
			{
				urubu.TranslationY=0;
			}
			protected override void OnSizeAllocated(double w, double h)
			{
				base.OnSizeAllocated(w, h);
				LarguraJanela = w;
				AlturaJanela = h;
			}
			void GerenciaCanos()
			{
				imgcactocima.TranslationY-= Velocidade;
				imgcactobaixo.TranslationX-= Velocidade;
				if(imgcactobaixo.TranslationX<-LarguraJanela)
				{
					imgcactobaixo.TranslationX = 0;
					imgcactocima.TranslationX = 0;
				}
			}
}

