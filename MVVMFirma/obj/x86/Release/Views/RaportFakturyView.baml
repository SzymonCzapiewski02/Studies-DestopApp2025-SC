   M S B A M L   `   `   `  ����   	MVVMFirma'    !MVVMFirma.Views.RaportFakturyView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources   �� MergedDictionaries �� Source��  �� $! StyleWszystkieViewBase.xaml?��� $ StyleJedenViewBase.xaml?�%    .��� 
�������$	��800��$
��Left=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$'��!Raport Faktury w wybranym okresie��$��3q�$��Center=�$
��Bold"�$��20(�� $��1��$��5q� � ColumnDefinitions �� $��2* ��� $��4* ��� $��4* ��� $��1* �.+��� $��0��$��Klient:��$
��Bold"�$��14(�$��5q��� $��1��$��3q�{�.     RoundedTextBoxStyle#��[  ����  �� Path$ 
KlientImie� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors . �� $��2��$��3q�{�.  #��[  ���� $ KlientNazwisko�$ PropertyChanged=� . �� $	��...��$��3��$��0��$��4,8,4,8q����� *ShowKlientci+� $��2��$��5q� �� $��2* ��� $��8* ��� $��1* �.+��� $��0��$��
Kategoria:��$
��Bold"�$��14(�$��5q��� $��1��$��3q�{�.  #��[  ���� $ KategoriaNazwa�$ PropertyChanged=� . �� $	��...��$��2��$��0��$��4,8,4,8q����� *ShowKategorie+� $��3��$��5q� �� $��2* ��� $��4* ��� $��4* ��� $��1* �.+��� $��0��$��Magazyn:��$
��Bold"�$��14(�$��5q��� $��1��$��3q�{�.  #��[  ���� $ MagazynNazwa�$ PropertyChanged=� . �� $��2��$��3q�{�.  #��[  ���� $ MagazynLokalizacja�$ PropertyChanged=� . �� $	��...��$��3��$��0��$��4,8,4,8q����� *ShowMagazynu+� $��4��$��5q� �� $��* ��� $��* �.+��� $��0��$��3q� �� CornerRadius$ 3��
����  �$	��0.3q��� $��1��$��3q�$ 3��
����  �$	��0.3q�� $��0��$��5q� �� $��* ��� $��* ��� $��* � �� $��4* ��� $��6* �.+��� $��0��$��0��$
��Vat:��$
��Bold"�$��14(�$��5q��� $��0��$��3q�$��1��#��[  ���� *Vat+	 �� Mode$	 TwoWay=��� $��1��$��0��$��Minumalna Kwota:��$
��Bold"�$��14(�$��5q��� $��1��$��3q�$��1��#��[  ���� *MinimalnaKwota+$	 TwoWay=��� $��2��$��0��$��Maksymalna Kwota:��$
��Bold"�$��14(�$��5q��� $��2��$��3q�$��1��#��[  ���� *MaksymalnaKwota+$	 TwoWay=�� $��1��$��5q� �� $��* ��� $��* ��� $��* � �� $��5* ��� $��6* �.+��� $��0��$��0��$��Data Wystawienia (Od):��$
��Bold"�$��14(�$��5q�(  "System.Windows.Controls.DatePicker  $��3q�$��0��$��1��
   SelectedDate
 �� *DataOd+�� $��1��$��0��$��Data Wystawienia (Do):��$
��Bold"�$��14(�$��5q�  $��3q�$��1��$��1��
 �� *DataDo+�� $��2��$��0��$��Status Płatności:��$
��Bold"�$��14(�$��5q��� $��2��$��3q�$��1�� �� SelectedValuePath$ Content�� �� SelectedValue �� *CzyOplacone+$	 TwoWay=�.@��� $
��True���� $��False��� $��5�� �� $��* ��� $��* �.+��� $��0��$��Generuj Raport Faktur��$	��250��$��3q�$��Center=����� *GenerujCommand+�� $��1��$��Generuj Raport Klienta��$	��250��$��3q�$��50��$��Center=����� *GenerujCommandK+&   System.Windows.Controls.DataGrid  $��6��   AutoGenerateColumns . $��3q����� *Raport+  $��7�� . $��3q����� *RaportKlienta+