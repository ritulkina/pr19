using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int size = 3;
        char[,] field;
        char currentPlayer = 'X';
        bool gameOver = false;


        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        void StartGame()
        {
            field = new char[size, size];
            currentPlayer = 'X';
            gameOver = false;

            BoardGrid.Children.Clear();
            BoardGrid.RowDefinitions.Clear();
            BoardGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < size; i++)
            {
                BoardGrid.RowDefinitions.Add(
                new RowDefinition());

                BoardGrid.ColumnDefinitions.Add(
                new ColumnDefinition());
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button button = new Button();

                    button.FontSize = 32;
                    button.FontWeight = FontWeights.Bold;
                    button.Margin = new Thickness(2);
                    button.Background = Brushes.White;

                    button.Click += Button_Click;

                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);

                    BoardGrid.Children.Add(button);
                }
            }

            StatusText.Text = "Ход: X";
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver)
                return;

            Button button = (Button)sender;

            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);

            if (field[row, col] != '\0')
                return;

            field[row, col] = currentPlayer;
            button.Content = currentPlayer.ToString();

            if (CheckWinner())
            {
                StatusText.Text = "Победил: " + currentPlayer;
                gameOver = true;
                return;
            }

            if (CheckDraw())
            {
                StatusText.Text = "Ничья!";
                gameOver = true;
                return;
            }

            if (currentPlayer == 'X')
                currentPlayer = 'O';
            else
                currentPlayer = 'X';

            StatusText.Text = "Ход: " + currentPlayer;
        }

        bool CheckWinner()
        {
            // Проверяем строки
            for (int i = 0; i < size; i++)
            {
                bool win = true;

                for (int j = 0; j < size; j++)
                {
                    if (field[i, j] != currentPlayer)
                    {
                        win = false;
                        break;
                    }
                }

                if (win)
                    return true;
            }

            // Проверяем столбцы
            for (int j = 0; j < size; j++)
            {
                bool win = true;

                for (int i = 0; i < size; i++)
                {
                    if (field[i, j] != currentPlayer)
                    {
                        win = false;
                        break;
                    }
                }

                if (win)
                    return true;
            }

            // Главная диагональ
            bool diagonal = true;

            for (int i = 0; i < size; i++)
            {
                if (field[i, i] != currentPlayer)
                {
                    diagonal = false;
                    break;
                }
            }

            if (diagonal)
                return true;

            // Побочная диагональ
            diagonal = true;

            for (int i = 0; i < size; i++)
            {
                if (field[i, size - 1 - i] != currentPlayer)
                {
                    diagonal = false;
                    break;
                }
            }

            return diagonal;
        }

        bool CheckDraw()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j] == '\0')
                        return false;
                }
            }

            return true;
        }

        private void RestartButton_Click(
        object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void SizeBox_SelectionChanged(
        object sender, SelectionChangedEventArgs e)
        {
            if (SizeBox.SelectedIndex < 0)
                return;

            size = SizeBox.SelectedIndex + 3;

            StartGame();
        }
    }
}