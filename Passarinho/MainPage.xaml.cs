namespace Passarinho;

public partial class MainPage : ContentPage
{
	const int Gravidade = 1;
	const int TempoEntreFrames = 25;//ms
	bool EstaMorto = false;

	public MainPage()
	{
		InitializeComponent();
	}
    void AplicaGravidade()
	{
		urubu.transactionY+=Gravidade; 
	}
	
	async Task Desenha()
	{
		while (!EstaMorto)
		{
			AplicaGravidade();
			await Task.Delay(TempoEntreFrames);
		}
	}
	void OnGameOverClicked(object s, TappedEventArgs a)
	{
		FrameGameOver.IsVisible=false;
		Inicializar();
		Desenha();
			}
			void Inicializar()
			{
				urubu.TranslationY=0;
			}
}

