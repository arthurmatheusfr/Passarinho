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
	protected override void OnApearing()
	{
		base.OnAppearing();
		Desenha();
	}
}

