using jarcosS5.Models;
using jarcosS5.Repositories;

namespace jarcosS5.Views;

public partial class vPersona : ContentPage
{
    private readonly PersonaRepository _repository;
    private Persona _personaEdicion = null;
	public vPersona()
	{
		InitializeComponent();
        // Usar la instancia compartida en App si ya existe para evitar llamar al constructor que requiere dbPath
        _repository = App._personRepo;
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        string nombre = txtName.Text?.Trim();
        if (string.IsNullOrEmpty(nombre))
        {
            lblStatus.Text = "El nombre no puede estar vacío.";
            lblStatus.TextColor = Colors.Red;
            return;
        }

        var nueva = new Persona { Name=nombre};
        App._personRepo.AddPerson(txtName.Text);
        lblStatus.Text = App._personRepo.Status;
        txtName.Text = string.Empty;

       RefrescarLista();
    }

    private void btnListar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        List<Persona> people = App._personRepo.GetAllPerson();
        listaPersonas.ItemsSource = people;
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        if (_personaEdicion == null) return;

        string nuevoNombre = txtName.Text?.Trim();

        if (string.IsNullOrEmpty(nuevoNombre))
        {
            lblStatus.Text = "Escribe un nombre para actualizar.";
            lblStatus.TextColor = Colors.OrangeRed;
            return;
        }

        _personaEdicion.Name = nuevoNombre;
        _repository.Update(_personaEdicion);

        lblStatus.Text = $"Actualizado: {_personaEdicion.Id} - {nuevoNombre}";
        lblStatus.TextColor = Colors.Green;

        SalirModoEdicion();
        RefrescarLista();
    }

    private void btnEditar_Clicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Persona persona)
        {
            _personaEdicion = persona;
            txtName.Text = persona.Name;

            // Activar modo edición
            btnActualizar.IsEnabled = true;
            btnAgregar.IsEnabled = false;
            btnCancelar.IsVisible = true;

            lblStatus.Text = $"Editando id: {persona.Id} — {persona.Name}";
            lblStatus.TextColor = Colors.DarkOrange;
        }
    }
    private void RefrescarLista()
    {
        listaPersonas.ItemsSource = null;
        listaPersonas.ItemsSource = _repository.GetAllPerson();
    }

    private void SalirModoEdicion()
    {
        _personaEdicion = null;
        txtName.Text = string.Empty;
        btnActualizar.IsEnabled = false;
        btnAgregar.IsEnabled = true;
        btnCancelar.IsVisible = false;
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        SalirModoEdicion();
        lblStatus.Text = "Edición cancelada.";
        lblStatus.TextColor = Colors.Gray;
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is Persona persona)
        {
            bool confirmar = await DisplayAlert(
                "Eliminar",
                $"¿Deseas eliminar a '{persona.Name}'?",
                "Sí", "No");

            if (!confirmar) return;

            _repository.Delete(persona);

            lblStatus.Text = $"Eliminado: {persona.Id} - {persona.Name}";
            lblStatus.TextColor = Colors.DarkRed;

            RefrescarLista();
        }
    }
}