   M S B A M L   `   `   `  ����I  EFootballLeague, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FootballLeague.ViewsFootballLeague.Views  5      #    FootballLeague.Views.AuthView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:FootballLeague.Views     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5	      
�������.��5      �   � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   (�  (� Stretch$ Uniform=�6   .��5      �� $��Center=�6   $��Center=�60   ._�5      �  � RowDefinitions 5      �� 5      $
��Auto �6(   �� 5      $��* �6(   .+�5      (� $ Uniform=�6   .��5      �� .+�5      �� $	��150��6'   $��30��5   '   $��15(�5   '   $��Center=�5   '   $��Center=�5   '   $��Center=�5    '   $��	0,50,0,30q�5!   '   $��DemiBold"�5"   '    �� SelectedItem 5#   '   �� *SelectedLanguage+��5$   '   �� *	Languages+�� 5&       �� Orientation$ 
Horizontal=�6)   $��Center=�5'      $��Center=�5(      $��0��5)      .+�5*   "   ��  	  Login#���   6)   
�������$��0,0,0,3q�5/      $	��200��50      $��50��51      $��DemiBold"�52      $��25(�53      $��Center=�54       � Cursor$
 Hand��55      $��0,0,10,0q�56      ��5+      �� *ShowLoginView+��5,      �� *LoginButtonColor+��5.      �� *LoginButtonColor+56      �� 58   "    
 SignUp#���  6)   $	��200��5:      $��50��5;      $��DemiBold"�5<      
�������$��0,0,0,3q�5@      $��25(�5A      $��Center=�5B      $
 Hand��5C      $��10,0,0,0q�5D      ��59      �� *ShowSignUpView+��5=      �� *SignUpButtonColor+��5?      �� *SignUpButtonColor+5D      5E      5F      5G      (� 5I      $ Uniform=�6   $��1��5J      .��5K      �� ��6)   �� *CurrentLeftPanelView+5L      5M      5N      5O      (� 5Q   
   $��1��6   $
 Fill=�6"   .��5R   
   �� 
����p�� �� CornerRadius$ 	80,0,0,80��5S      ._�5T      �� $��Center=�6   $��Center=�5U      .+�5W      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Trophy=�6   $	��400��5X      $	��400��5Y      
��� ���$��
30,30,30,0q�5[      �� 5]        WelcomeBack#���  6   $��50(�5^      $��DemiBold"�5_      
�������$��Center=�5a      $��0,0,0,30q�5b      5c      5d      5e      5f      5g      