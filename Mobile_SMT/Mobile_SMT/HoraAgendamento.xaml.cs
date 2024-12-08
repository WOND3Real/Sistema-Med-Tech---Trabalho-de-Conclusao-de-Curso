using Microsoft.Maui.Controls;
using System;

namespace Mobile_SMT
{
    public partial class HoraAgendamento : ContentPage
    {
        public HoraAgendamento()
        {
            InitializeComponent();
            var currentDate = DateTime.Now;
            Console.WriteLine($"[INFO] Página carregada. Data atual: {currentDate}");
            GenerateCalendar(currentDate.Month, currentDate.Year);
        }

        private void GenerateCalendar(int month, int year)
        {
            Console.WriteLine($"[INFO] Gerando calendário para {month}/{year}");
            CalendarGrid.Children.Clear();
            CalendarGrid.RowDefinitions.Clear();
            CalendarGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < 7; i++)
            {
                CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }

            for (int i = 0; i < 7; i++)
            {
                CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            string[] weekDays = { "Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb" };

            for (int i = 0; i < 7; i++)
            {
                var label = new Label
                {
                    Text = weekDays[i],
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Microsoft.Maui.Graphics.Colors.Black
                };
                Grid.SetRow(label, 0);
                Grid.SetColumn(label, i);
                CalendarGrid.Children.Add(label);
            }

            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);

            int currentRow = 1;

            for (int day = 1; day <= daysInMonth; day++)
            {
                int column = (day + (int)firstDayOfMonth.DayOfWeek - 1) % 7;
                if (day == 1 || column == 0 && day > 1)
                {
                    currentRow++;
                }

                var button = new Button
                {
                    Text = day.ToString(),
                    BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
                    TextColor = Microsoft.Maui.Graphics.Colors.Black,
                    BorderColor = Microsoft.Maui.Graphics.Colors.Transparent,
                    BorderWidth = 1,
                    CornerRadius = 0,
                    CommandParameter = new DateTime(year, month, day) // Alterado para DateTime
                };
                button.Clicked += OnDateButtonClicked;

                Grid.SetRow(button, currentRow);
                Grid.SetColumn(button, column);
                CalendarGrid.Children.Add(button);
            }
        }

        private void OnDateButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var selectedDate = (DateTime)button.CommandParameter; // Alterado para DateTime

            foreach (var child in CalendarGrid.Children)
            {
                if (child is Button dayButton)
                {
                    dayButton.BackgroundColor = Microsoft.Maui.Graphics.Colors.White;
                }
            }

            button.BackgroundColor = Microsoft.Maui.Graphics.Colors.Orange;
            SharedData.DataSelecionada = selectedDate; // Armazenando a data como DateTime

            Console.WriteLine($"[INFO] Data selecionada: {SharedData.DataSelecionada}");
            ShowTimePicker();
        }

        private async void ShowTimePicker()
        {
            Console.WriteLine("[INFO] Exibindo seletor de horário...");
            
            var timePicker = new TimePicker
            {
                Time = new TimeSpan(12, 0, 0) // Hora padrão
            };

            var dialog = new ContentPage
            {
                Content = new StackLayout
                {
                    Padding = 20,
                    Children =
                    {
                        new Label { Text = "Selecione um horário:", FontAttributes = FontAttributes.Bold },
                        timePicker,
                        new Button
                        {
                            Text = "Confirmar",
                            Command = new Command(() =>
                            {
                                SharedData.HoraSelecionada = timePicker.Time; // Hora selecionada como TimeSpan
                                Console.WriteLine($"[INFO] Horário selecionado: {SharedData.HoraSelecionada}");
                                Application.Current.MainPage.Navigation.PopModalAsync();
                            })
                        }
                    }
                }
            };

            await Navigation.PushModalAsync(dialog);
        }

        private async void OnAgendarButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("[INFO] Botão de agendamento clicado.");
            Console.WriteLine($"Data: {SharedData.DataSelecionada}, Hora: {SharedData.HoraSelecionada}");
            await Navigation.PushAsync(new PagamentoEscolha());
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("[INFO] Botão de voltar clicado.");
            await Navigation.PopAsync();
        }
    }
}
