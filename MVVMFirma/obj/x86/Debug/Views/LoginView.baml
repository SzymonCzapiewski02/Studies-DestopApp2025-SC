   M S B A M L   `   `   `  ����D  @MVVMFirma, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MVVMFirma.ViewsMVVMFirma.Views  5   	       MVVMFirma.Views.LoginView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MVVMFirma.Views     5   	     � Title$  	LoginView��5   	   $	��250��6   $	��400��6(   .��5	      � .+�5
   
   �� $��Nazwa użytkownika:��6   $��	10,30,0,0q�6.   $
��Left=�6A   $	��Top=�6\   �� 5   
   -   5         ��Name$ UsernameBox��5      $��150,30,10,0q�6f   $
��Left=�6{   $	��Top=�6�   $	��200��6�   ��6'   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��Hasło:��6   $��	10,80,0,0q�6"   $
��Left=�65   $	��Top=�6P   G� 5   
   -   5         G�Name$ PasswordBox��5      $��150,80,10,0q�6+   $
��Left=�6@   $	��Top=�6[   $	��200��6s   6   �� 5   
   $��Zaloguj��6   $��150,130,0,0q�6D   $
��Left=�6Y   $	��Top=�6t   $	��100��6�   ��6#   �� *LoginCommand+6�   5      5      