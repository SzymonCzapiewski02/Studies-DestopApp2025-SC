   M S B A M L   `   `   `  ����D  @MVVMFirma, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MVVMFirma.ViewsMVVMFirma.Views  5      #    MVVMFirma.Views.NowyAdresView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:MVVMFirma.Views     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $ StyleJedenViewBase.xaml?�6%   %    5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $	��250 �6   �� 5      $	��150 �6   �� 5      $	��250 �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   �� $��0��6   $��0��6       RoundedLaberBoxStyle#��[  6-   $��Ulica��6[   �� 5   
   $��1��6   $��0��6"     RoundedTextBoxStyle#��[ 6/   ��6\   ��  �� Path$ Ulica� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors . �� 5   
   $��0��6   $��1��6    #��[  6-   $��Miasto��6[   �� 5   
   $��1��6   $��1��6"   #��[ 6/   ��6\   �� $ Miasto�$ PropertyChanged=� . �� 5   
   $��2��6   $��0��6    #��[  6-   $��Kod Pocztowy��6[   �� 5    
   $��3��6   $��0��6"   #��[ 6/   ��6\   �� $ KodPocztowy�$ PropertyChanged=� . �� 5!   
   $��2��6   $��1��6    #��[  6-   $
��Kraj��6[   �� 5"   
   $��3��6   $��1��6"   #��[ 6/   ��6\   �� $
 Kraj�$ PropertyChanged=� . 5#      5$      