   M S B A M L   `   `   `  ����I  EFootballLeague, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FootballLeague.ViewsFootballLeague.Views  5      0    *FootballLeague.Views.AdministratorHomeView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:FootballLeague.Views     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5	      
�������   GlobalFontStyle#���   5
      .��5      �   � ColumnDefinitions  5   
   �� 5      $��0.25* �6   �� 5      $��0.75* �6   .+�5   
   ��   PrimaryBrush#���  6    �� CornerRadius$ 	0,50,50,0��5      ._�5      (�  (� Stretch$ Uniform=�6   .��5      �� $��20q�6   $��Center=�5      .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Trophy=�6"   $	��350��5      $	��200��5        SecondaryBrush#���  5      $��Center=�5      $��Center=�5      $��10,10,10,70q�5      �� 5      $��25(�5       $��DemiBold"�5!      #���  5"        PrimaryTextBrush#���  5#      $��80��5$      $��3q�5%      $
��0,10q�5&       � Cursor$
 Hand��5'      ��5      �� *ShowAccountsCommand+.��5(      ��  �� Orientation$ 
Horizontal=�6%   $��Center=�6>   .+�5)        $ AccountGroup=�6*   #���  5*   )   $��50��5+   )   $��50��5,   )   $��0,0,10,0q�5-   )   $��Center=�5.   )   �� 5/        Accounts#���  6(   $��Center=�50   )   #���  51   )   52      53      �� 55      $��25(�56      $��DemiBold"�57      #���  58      #���  59      $��80��5:      $
��0,10q�5;      $��3q�5<      $
 Hand��5=      ��55      �� *ShowGamesCommand+.��5>      �� $ 
Horizontal=�6%   $��Center=�6>   .+�5?        $ FootballPitch=�6*   #���  5@   !   $��50��5A   !   $��50��5B   !   $��0,0,10,0q�5C   !   $��Center=�5D   !   �� 5E        Matches#���  6(   $��Center=�5F   !   #���  5G   !   5H      5I      �� 5J      $��25(�5K      $��DemiBold"�5L      #���  5M      #���  5N      $��80��5O      $
��0,10q�5P      $��3q�5Q      $
 Hand��5R      ��5J      �� *ShowTeamsCommand+.��5S      �� $ 
Horizontal=�6%   $��Center=�6>   .+�5T        $ Badge=�6*   #���  5U   %   $��50��5V   %   $��50��5W   %   $��0,0,10,0q�5X   %   $��Center=�5Y   %   �� 5Z       	 Teams#���  6(   $��Center=�5[   !   #���  5\   !   5]      5^      �� 5`      $��25(�5a      $��DemiBold"�5b      #���  5c      #���  5d      $��80��5e      $��3q�5f      $
��0,10q�5g      $
 Hand��5h      ��5`      �� *ShowSettingsCommand+.��5i      �� $ 
Horizontal=�6%   $��Center=�6>   .+�5j        $
 Gear=�6*   #���  5k   !   $��50��5l   !   $��50��5m   !   $��0,0,10,0q�5n   !   $��Center=�5o   !   �� 5p        Settings#���  6(   $��Center=�5q   !   #���  5r   !   5s      5t      �� 5u      $��25(�5v      $��DemiBold"�5w      
���  ��#���  5y      $��80��5z      $��3q�5{      $
��0,10q�5|      $
 Hand��5}      ��5u      �� *ShowLoginCommand+.��5~      �� $ 
Horizontal=�6%   $��Center=�6>   .+�5        $ Logout=�6*   #���  5�   %   $��50��5�   %   $��50��5�   %   $��0,0,10,0q�5�   %   $��Center=�5�   %   �� 5�       
 LogOut#���  6(   $��Center=�5�   !   #���  5�   !   5�      5�      5�      5�      5�      �� 5�   
   $��1��6   ._�5�      �� ��6   �� *CurrentRightPanelView+5�      5�      5�      