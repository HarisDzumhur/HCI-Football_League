   M S B A M L   `   `   `  ����I  EFootballLeague, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FootballLeague.ViewsFootballLeague.Views  5      %    FootballLeague.Views.SignUpView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:FootballLeague.Views     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5	      
�������.��5
      �   � RowDefinitions  5   
   �� 5      $	��100 �6   �� 5      $	��600 �6   .+�5   
   �� $��0��6   ._�5      ��    RequestSignUp#���   6   $��40(�5      $
��Bold"�5      
����p��$��Center=�5      $��Bottom=�5      $
��Wrap=�5      5      �� 5   
   $��1��6   ._�5      �� $	��Top=�6   $��Center=�5      $��0,50,0,0q�5      .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5       '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Account=�6"   $��30��5!   "   $��30��5"   "   
����p��$��Center=�5$   "   �� 5%      $	��400��6     
PromptName)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5&      $��40��5(      $��20(�5)      $��10q�5*      $��DemiBold"�5+      $��Center=�5,      $��Center=�5-      ��5'      �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ OneWayToSource=�5-      5.      �� 50      $ 
Horizontal=�6   .+�51        $ AccountGroup=�6"   $��30��52      $��30��53      
����p��$��Center=�55      �� 56      $	��400��6     PromptSurname# �  57      $��40��59      $��20(�5:      $��10q�5;      $��DemiBold"�5<      $��Center=�5=      $��Center=�5>      ��58      �� *	Surname+$ PropertyChanged=�$ OneWayToSource=�5>      5?      �� 5A      $ 
Horizontal=�6   $��0,40,0,0q�66   .+�5B        $ AccountOutline=�6"   $��30��5C      $��30��5D      
����p��$��Center=�5F      �� 5G      $	��400��6     PromptUsername# �  5H      $��40��5J      $��10q�5K      $��20(�5L      $��DemiBold"�5M      $��Center=�5N      $��Center=�5O      ��5I      �� *
Username+$ PropertyChanged=�$ OneWayToSource=�5O      5P      �� 5R      $ 
Horizontal=�6   .+�5S        $ LockOutline=�6"   $��30��5T      $��30��5U      
����p��$��Center=�5W      G� 5X      $	��400��5Y   "     PromptPassword# �  5Z   "   $��40��5[   "   $��20(�5\   "   $��10q�5]   "   $��DemiBold"�5^   "   $��Center=�5_   "   $��Center=�5`   "   0  *MaterialDesignThemes.Wpf.PasswordBoxAssist   Password 5X   "   �� *
Password+$ PropertyChanged=�$ OneWayToSource=�5`   "   5a      �� 5c      $ 
Horizontal=�6   .+�5d        $ LockCheckOutline=�6"   $��30��5e      $��30��5f      
����p��$��Center=�5h      G� 5j      $	��400��5k   "     PromptRepeatPassword# �  5l   "   $��10q�5m   "   $��40��5n   "   $��20(�5o   "   $��DemiBold"�5p   "   $��Center=�5q   "   $��Center=�5r   "    5j   "   �� *PasswordConfirmation+$ PropertyChanged=�$ OneWayToSource=�5r   "   5s      �� 5u       
 SignUp#���  6   $	��300��5w      $��50��5x      $��DemiBold"�5y      
����p��
�������$��30(�5|      $��Center=�5}      $��0, 50, 0, 0q�5~       � Cursor$
 Hand��5      ��5v      �� *SignUpCommand+5      5�      5�      5�      5�      