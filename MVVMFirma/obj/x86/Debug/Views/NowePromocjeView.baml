   M S B A M L   `   `   `  ����D  @MVVMFirma, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MVVMFirma.ViewsMVVMFirma.Views  5      &     MVVMFirma.Views.NowePromocjeView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:MVVMFirma.Views     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $ StyleJedenViewBase.xaml?�6%   %    5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $	��160 �6   �� 5      $	��250 �6   �� 5      $��50 �6   �� 5      $	��160 �6   �� 5      $	��250 �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   �� $��0��6   $��0��6       RoundedLaberBoxStyle#��[  6-   $��Produkt��6[   �� 5    
   $��1��6   $��0��6"   {�.    RoundedTextBoxStyle#��[ 6A   ��6n   ��  �� Path$ ProduktNazwa� �� UpdateSourceTrigger$ PropertyChanged=��� 5!   
   $	��...��6   $��2��6   $��0��6/   $��4,8,4,8q�6<   ��6M   �� *ShowProdukty+�� 5"   
   $��0��6   $��1��6    #��[  6-   $��Data Rozpoczecia��6[   (  "System.Windows.Controls.DatePicker  5#   
   $��1��6   $��1��6%     RoundedDatePickerStyle#��[ 62      SelectedDate 6b   �� $ DataRozpoczecia�$ PropertyChanged=� �� ValidatesOnDataErrors . �� 5$   
   $��3��6   $��0��6    #��[  6-   $��Data Zakonczenia��6[     5%   
   $��4��6   $��0��6%   #��[ 62    6b   �� $ DataZakonczenia�$ PropertyChanged=� . �� 5&   
   $��3��6   $��1��6    #��[  6-   $��NowaCena��6[   �� 5'   
   $��4��6   $��1��6"   #��[ 6/   ��6\   �� $ NowaCena�$ PropertyChanged=� . 5(      5)      