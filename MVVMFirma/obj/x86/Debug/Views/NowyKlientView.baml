   M S B A M L   `   `   `  ����D  @MVVMFirma, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MVVMFirma.ViewsMVVMFirma.Views  5      $    MVVMFirma.Views.NowyKlientView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:MVVMFirma.Views     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $ StyleJedenViewBase.xaml?�6%   %    5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $	��160 �6   �� 5      $	��250 �6   �� 5      $	��160 �6   �� 5      $	��250 �6   �� 5      $��50 �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   �� $��0��6   $��0��6       RoundedLaberBoxStyle#��[  6-   $
��Imie��6[   �� 5    
   $��1��6   $��0��6"     RoundedTextBoxStyle#��[ 6/   ��6\   ��  �� Path$
 Imie� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors . �� 5!   
   $��0��6   $��1��6    #��[  6-   $��Nazwisko��6[   �� 5"   
   $��1��6   $��1��6"   #��[ 6/   ��6\   �� $ Nazwisko�$ PropertyChanged=� . �� 5#   
   $��0��6   $��2��6    #��[  6-   $��Email��6[   �� 5$   
   $��1��6   $��2��6"   #��[ 6/   ��6\   �� $ Email�$ PropertyChanged=� . �� 5%   
   $��0��6   $��3��6    #��[  6-   $��Telefon��6[   �� 5&   
   $��1��6   $��3��6"   #��[ 6/   ��6\   �� $ Telefon�$ PropertyChanged=� . �� 5'   
   $��2��6   $��0��6    #��[  6-   $��Adresu��6[   �� 5(   
   $��3��6   $��0��6"   {�.  #��[ 6A   ��6n   �� $ 
AdresUlica�$ PropertyChanged=��� 5)   
   $	��...��6   $��4��6   $��0��6/   $��4,8,4,8q�6<   ��6M   �� *
ShowAdresy+�� 5*   
   $��2��6   $��1��6    #��[  6-   $��Data Rejestracji��6[   (  "System.Windows.Controls.DatePicker  5+   
   $��3��6   $��1��6%     RoundedDatePickerStyle#��[ 62      SelectedDate 6b   �� $ DataRejestracji�$ PropertyChanged=� . �� 5,   
   $��2��6   $��2��6    #��[  6-   $��Haslo��6[   �� 5-   
   $��3��6   $��2��6"   #��[ 6/   ��6\   �� $ Haslo�$ PropertyChanged=� . 5.      5/      