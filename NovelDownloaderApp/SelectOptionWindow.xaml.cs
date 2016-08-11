using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using moe.Jixun.model;

namespace moe.Jixun
{
    /// <summary>
    /// Interaction logic for SelectOptionWindow.xaml
    /// </summary>
    public partial class SelectOptionWindow : Window
    {
        private RadioButton _selected;
        private bool _confirm;
        private readonly List<RadioButton> _options = new List<RadioButton>();
        private readonly SelectOptionAnswer _answer = new SelectOptionAnswer();

        public SelectOptionWindow(string title, List<string> itemNames, int defaultIndex, bool checkDoNotAsk)
        {
            InitializeComponent();
            _answer.DoNotAsk = checkDoNotAsk;

            DataContext = _answer;

            Title = $"请选择一项 {title}";

            // 将所有项目加入到列表
            foreach (var name in itemNames)
            {
                var radio = new RadioButton
                {
                    Content = name
                };

                radio.Checked += RadioChecked;

                _options.Add(radio);
                Options.Children.Add(radio);
            }

            if (_options.Count == 0) return;

            // 勾选默认选项
            if (defaultIndex < 0 || defaultIndex >= _options.Count)
                defaultIndex = 0;

            _options[defaultIndex].IsChecked = true;
            _selected = _options[defaultIndex];
        }

        private void RadioChecked(object sender, RoutedEventArgs e)
        {
            _selected = (RadioButton) sender;
        }

        internal SelectOptionAnswer GetResult()
        {
            _answer.Cancel = !_confirm;
            _answer.Index = _options.IndexOf(_selected);
            return _answer;
        }

        private void BtnOkClicked(object sender, RoutedEventArgs e)
        {
            _confirm = true;
            Close();
        }
    }
}
