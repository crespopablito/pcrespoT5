namespace pcrespoT5.Views;

public partial class vPersona : ContentPage
{
    public vPersona()
    {
        InitializeComponent();
    }

    // INSERTAR
    private void btnAgregar_Clicked(object? sender, EventArgs e)
    {
        lblStatusMesage.Text = "";
        App._personRepo.AddNewPerson(txtName.Text ?? string.Empty);
        lblStatusMesage.Text = App._personRepo.StatusMessage;
    }

    // SELECCIONAR
    private void btnMostrar_Clicked(object? sender, EventArgs e)
    {
        List<Modelos.Persona> people = App._personRepo.GetAllPerson();
        listarPersonas.ItemsSource = people;
    }

    // ELIMINAR
    private async void btnEliminar_Clicked(object? sender, EventArgs e)
    {
        if (sender is not Button boton ||
            boton.CommandParameter is not Modelos.Persona persona)
            return;

        bool confirmar = await DisplayAlertAsync(
            "Confirmar eliminación",
            "¿Está seguro de eliminar esta persona?",
            "Sí",
            "No");

        if (confirmar)
        {
            App._personRepo.DeletePerson(persona.Id);

            lblStatusMesage.Text = App._personRepo.StatusMessage;

            listarPersonas.ItemsSource = App._personRepo.GetAllPerson();
        }
    }

    // ACTUALIZAR
    private async void btnEditar_Clicked(object? sender, EventArgs e)
    {
        if (sender is not Button boton ||
            boton.CommandParameter is not Modelos.Persona persona)
            return;

        string? nuevoNombre = await DisplayPromptAsync(
            "Actualizar persona",
            "Ingrese el nuevo nombre:",
            "Guardar",
            "Cancelar",
            "Nombre",
            maxLength: 25,
            initialValue: persona.Name);

        if (nuevoNombre == null)
            return;

        bool actualizado = App._personRepo.UpdatePerson(
            persona.Id,
            nuevoNombre);

        lblStatusMesage.Text = App._personRepo.StatusMessage;

        if (actualizado)
        {
            listarPersonas.ItemsSource = App._personRepo.GetAllPerson();
        }
    }
}
