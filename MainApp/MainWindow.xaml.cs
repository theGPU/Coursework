using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MainApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) => PlaceholderWorker.RemovePlaceholder(sender, e);

        private void TextBox_LostFocus(object sender, RoutedEventArgs e) => PlaceholderWorker.AddPlaceholder(sender, e);

        private void TextBox_Initialized(object sender, EventArgs e) => PlaceholderWorker.OnInit(sender, e);

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => Utils.NumberValidationTextBox(sender, e);

        private void OnError(Label label)
        {
            label.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            label.Content = "Ошибка!";
        }

        private void OnResult(Label label, string result)
        {
            label.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            label.Content = result;
        }

        private void CalculationButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var buttonTag = (string)button.Tag;
            if (buttonTag == "1")
            {
                if (ParseInputBoxesInAnnuity(out var rate, out var payment, out var yeards))
                    OnResult(resultBox_1, Proxy.CalculateAnnuity(rate, payment, yeards).ToString() + " руб.");
                else
                    OnError(resultBox_1);
            }
            if (buttonTag == "2")
            {
                if (ParseInputBoxesInPercent(out var creditSize, out var firstPayment, out var secondPayment))
                    OnResult(resultBox_2, Proxy.CalculatePercents(creditSize, firstPayment, secondPayment).ToString() + "%");
                else
                    OnError(resultBox_2);
            }
            if (buttonTag == "3")
            {
                if (ParseInputBoxesInDifferentiated(out var _rate, out var months, out var paymentMonth, out var _payment))
                    OnResult(resultBox_3, Proxy.CalculateDifferentiated(_rate, months, paymentMonth, _payment).ToString() + " руб.");
                else
                    OnError(resultBox_3);
            }
        }

        private void SetNewErrorLabel(Label label, string errorMsg)
        {
            ErrorLabelsController.DeactiveErrorLabel(null, null, label);
            ErrorLabelsController.ActiveErrorLabel(label, errorMsg);
        }

        private static bool CheckBoxPassed(TextBox textBox)
        {
            return textBox.Text.Length >= (textBox.Text.Contains(".") ? 3 : 1) && !textBox.GetPlaceholderActive();
        }

        private bool ParseInputBoxesInAnnuity(out double rate, out double payment, out byte yeards)
        {
            rate = 0; payment = 0; yeards = 0;

            var rateBoxPass = CheckBoxPassed(rateBox_1);
            if (!rateBoxPass)
                SetNewErrorLabel(rateErrorLabel_1, "Необходимо ввести процентную ставку крудита");
            else
                if (!double.TryParse(rateBox_1.Text, out rate))
                {
                    SetNewErrorLabel(rateErrorLabel_1, "Установлено слишком большое/маленькое число");
                    rateBoxPass = false;
                }

            var paymentBoxPass = CheckBoxPassed(paymentBox_1);
            if (!paymentBoxPass)
                SetNewErrorLabel(paymentErrorLabel_1, "Необходимо ввести сумму годового платежа");
            else
                if (!double.TryParse(paymentBox_1.Text, out payment))
                {
                    SetNewErrorLabel(paymentErrorLabel_1, "Установлено слишком большое/маленькое число");
                    paymentBoxPass = false;
                }

            var yeardsBoxPass = CheckBoxPassed(yeardsBox_1);
            if (!yeardsBoxPass)
                SetNewErrorLabel(yeardsErrorLabel_1, "Необходимо ввести количество лет");
            else
                if (!byte.TryParse(yeardsBox_1.Text, out yeards))
                {
                    SetNewErrorLabel(yeardsErrorLabel_1, "Установлено слишком большое/маленькое число");
                    yeardsBoxPass = false;
                }

            return rateBoxPass && paymentBoxPass && yeardsBoxPass;
        }

        private bool ParseInputBoxesInPercent(out ulong creditSize, out ulong firstPayment, out ulong secondPayment)
        {
            creditSize = 0; firstPayment = 0; secondPayment = 0;

            var creditSizeBoxPassed = CheckBoxPassed(creditSizeBox_2);
            if (!creditSizeBoxPassed)
                SetNewErrorLabel(creditSizeErrorLabel_2, "Необходимо ввести сумму кредита");
            else if (!ulong.TryParse(creditSizeBox_2.Text, out creditSize))
            {
                SetNewErrorLabel(creditSizeErrorLabel_2, "Установлено слишком большое/маленькое число");
                creditSizeBoxPassed = false;
            }

            var firstPaymentBoxPassed = CheckBoxPassed(firstPaymentBox_2);
            if (!firstPaymentBoxPassed)
                SetNewErrorLabel(firstPaymentErrorLabel_2, "Необходимо ввести сумму первого платежа");
            else if (!ulong.TryParse(firstPaymentBox_2.Text, out firstPayment))
            {
                SetNewErrorLabel(firstPaymentErrorLabel_2, "Установлено слишком большое/маленькое число");
                firstPaymentBoxPassed = false;
            }

            var secondPaymentBoxPassed = CheckBoxPassed(secondPaymentBox_2);
            if (!secondPaymentBoxPassed)
                SetNewErrorLabel(secondPaymentErrorLabel_2, "Необходимо ввести сумму второго платежа");
            else if (!ulong.TryParse(secondPaymentBox_2.Text, out secondPayment))
            {
                SetNewErrorLabel(secondPaymentErrorLabel_2, "Установлено слишком большое/маленькое число");
                secondPaymentBoxPassed = false;
            }

            var allPoxesPassed = creditSizeBoxPassed && firstPaymentBoxPassed && secondPaymentBoxPassed;
            if (allPoxesPassed && firstPayment + secondPayment < creditSize)
            {
                SetNewErrorLabel(creditSizeErrorLabel_2, "Сумма кредита должна быть меньше или равна суммы платежей");
                SetNewErrorLabel(firstPaymentErrorLabel_2, "Сумма платежей должна быть больше или равна суммы кредита");
                SetNewErrorLabel(secondPaymentErrorLabel_2, "Сумма платежей должна быть больше или равна суммы кредита");
                return false;
            }

            return allPoxesPassed;
        }

        private bool ParseInputBoxesInDifferentiated(out double rate, out int months, out int paymentMonth, out double payment)
        {
            rate = 0; months = 0; paymentMonth = 0; payment = 0;

            var rateBoxPassed = CheckBoxPassed(rateBox_3);
            if (!rateBoxPassed)
                SetNewErrorLabel(rateErrorLabel_3, "Необходимо ввести процентнкю ставку крудита");
            else if (!double.TryParse(rateBox_3.Text, out rate))
            {
                SetNewErrorLabel(rateErrorLabel_3, "Установлено слишком большое/маленькое число");
                rateBoxPassed = false;
            }

            var monthsBoxPassed = CheckBoxPassed(monthsBox_3);
            if (!monthsBoxPassed)
                SetNewErrorLabel(monthsErrorLabel_3, "Необходимо ввести количество месяцев");
            else if (!int.TryParse(monthsBox_3.Text, out months))
            {
                SetNewErrorLabel(monthsErrorLabel_3, "Установлено слишком большое/маленькое число");
                monthsBoxPassed = false;
            }

            var monthBoxPassed = CheckBoxPassed(monthBox_3);
            if (!monthBoxPassed)
                SetNewErrorLabel(monthErrorLabel_3, "Необходимо ввести месяц известного платежа");
            else if (!int.TryParse(monthBox_3.Text, out paymentMonth))
            {
                SetNewErrorLabel(monthErrorLabel_3, "Установлено слишком большое/маленькое число");
                monthBoxPassed = false;
            }

            var paymentBoxPassed = CheckBoxPassed(paymentBox_3);
            if (!paymentBoxPassed)
                SetNewErrorLabel(paymentErrorLabel_3, "Необходимо ввести сумму известного платежа");
            else if (!double.TryParse(paymentBox_3.Text, out payment))
            {
                SetNewErrorLabel(paymentErrorLabel_3, "Установлено слишком большое/маленькое число");
                paymentBoxPassed = false;
            }

            Console.WriteLine(months);
            Console.WriteLine(paymentMonth);
            var allPoxesPassed = rateBoxPassed && monthsBoxPassed && monthBoxPassed && paymentBoxPassed;
            if (paymentBoxPassed && paymentMonth < 1)
            {
                SetNewErrorLabel(monthErrorLabel_3, "Месяц платежа не может быть меньше 1");
                allPoxesPassed = false;
            }
            if (monthsBoxPassed && months < 1)
            {
                SetNewErrorLabel(monthsErrorLabel_3, "Количество месяцев платежа не может быть меньше 1");
                allPoxesPassed = false;
            }
            if (monthBoxPassed && monthsBoxPassed && months < paymentMonth)
            {
                SetNewErrorLabel(monthsErrorLabel_3, "Количество месяцев не может быть меньше месяца платежа");
                SetNewErrorLabel(monthErrorLabel_3, "Месяц платежа не может быть больше количества месяцев");
                allPoxesPassed = false;
            }

            return allPoxesPassed;
        }
    }
}
