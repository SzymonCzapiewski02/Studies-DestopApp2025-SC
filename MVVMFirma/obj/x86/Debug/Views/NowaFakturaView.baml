   M S B A M L   `   `   `  ����D  @MVVMFirma, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MVVMFirma.ViewsMVVMFirma.Views  5      %    MVVMFirma.Views.NowaFakturaView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:MVVMFirma.Views     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $ StyleJedenViewBase.xaml?�6%   %    5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $	��160 �6   �� 5      $	��250 �6   �� 5      $��50 �6   �� 5      $	��160 �6   �� 5      $	��250 �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   �� $��0��6   $��0��6       RoundedLaberBoxStyle#��[  6-   $��
Zamowienia��6[   �� 5    
   $��1��6   $��0��6"   {�.    RoundedTextBoxStyle#��[ 6A   ��6n   ��  �� Path$ ZamowieniaNazwa� �� UpdateSourceTrigger$ PropertyChanged=��� 5!   
   $	��...��6   $��2��6   $��0��6/   $��4,8,4,8q�6<   ��6M   �� *ShowZamowienia+�� 5"   
   $��0��6   $��1��6    #��[  6-   $��Data Wystawienia��6[   (  "System.Windows.Controls.DatePicker  5#   
   $��1��6   $��1��6%     RoundedDatePickerStyle#��[ 62      SelectedDate 6b   �� $ DataWystawienia�$ PropertyChanged=� �� ValidatesOnDataErrors . �� 5$   
   $��0��6   $��2��6    #��[  6-   $��Kwota Całkowita��6[   �� 5%   
   $��1��6   $��2��6"   #��[ 6/   ��6\   �� $ KwotaCalkowita�$ PropertyChanged=� . �� 5&   
   $��0��6   $��3��6    #��[  6-   $��Termin Platnosci��6[     5'   
   $��1��6   $��3��6%   #��[ 62    6b   �� $ TerminPlatnosci�$ PropertyChanged=� . �� 5(   
   $��3��6   $��2��6    #��[  6-   $��Numer Faktury��6[   �� 5)   
   $��4��6   $��2��6"   #��[ 6/   ��6\   �� $ NumerFaktury�$ PropertyChanged=� . �� 5*   
   $��3��6   $��0��6    #��[  6-   $��
Pracownika��6[   �� 5+   
   $��4��6   $��0��6#     RoundedComboBoxStyle#��[ 60   	 �� DisplayMemberPath$	 Value��6�   
 �� SelectedValuePath$	
 Key��6�   ��6^   �� *PracownikKeyAndValue+ .  �� SelectedValue 6�   �� $ IdPracownika� �� Mode$ TwoWay=��� 5,   
   $��3��6   $��1��6    #��[  6-   $��Typ Faktury��6[   �� 5-   
   $��4��6   $��1��6"   #��[ 6/   ��6\   �� $ 
TypFaktury�$ PropertyChanged=� . �� 5.   
   $��3��6   $��3��6    #��[  6-   $��Waluta��6[   �� 5/   
   $��4��6   $��3��6"   #��[ 6/   ��6\   �� $ Waluta�$ PropertyChanged=� . 50      51      