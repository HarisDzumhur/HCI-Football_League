   M S B A M L   `   `   `  ����  FootballLeague$    FootballLeague.Views.LoginView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      
�������.��5	      �   � RowDefinitions  5
   
   �� 5      $	��100 �6    �� 5      $	��600 �6    .+�5      �� $��0��6   ._�5      ��    RequestLogin#���   6   $��40(�5      $
��Bold"�5      
����p��$��Center=�5      $��Bottom=�5      $
��Wrap=�5      5      �� 5   
   $��1��6   ._�5      �� $	��Top=�6   $��Center=�5      $��0,50,0,0q�5      .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ AccountOutline=�6"   $��30��5   "   $��30��5    "   
����p��$��Center=�5"   "   $��Center=�5#   "   �� 5$      $	��400��6     PromptUsername)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5%      $��10q�5&      $��40��5(      $��20(�5)      $��DemiBold"�5*      $��Center=�5+      $��Center=�5,      ��5'      �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ OneWayToSource=�5,      5-      �� 5/      $ 
Horizontal=�6   .+�50        $ LockOutline=�6"   $��30��51   "   $��30��52   "   
����p��$��Center=�54   "   $��Center=�55   "   G� 56        PromptPassword# �  57   "   $	��400��58   "   $��10q�59   "   $��40��5:   "   $��20(�5;   "   $��DemiBold"�5<   "   $��Center=�5=   "   $��Center=�5>   "   0  *MaterialDesignThemes.Wpf.PasswordBoxAssist   Password 56   "   �� *
Password+$ PropertyChanged=�$ OneWayToSource=�5>   "   5?      �� 5B        Role#���  6   $��	10,30,0,5q�5C       $��Italic$�5D       $��15(�5E       �� 5G      $ 
Horizontal=�6   $��Center=�5H   !   .+�5I      ��   Administrator#���  6   $��DemiBold"�5K   !   $	��150��5L   !   $��40��6-   
�������$��16(�5P   !   $��20q�5Q   !    � Cursor$
 Hand��5R   !   {�5J      �� *	IsEnabled+$ PropertyChanged=�$ TwoWay=���5M   !   �� *AdministratorButtonColor+��5N   !   �� *AdministratorOption+5R   !   �� 5T        ClassicUser#���  6   $	��150��5V   !   $��40��5W   !   $��DemiBold"�5X   !   
�������$��16(�5\   !   $��20q�5]   !   $
 Hand��5^   !   {�5U      �� *	IsEnabled+$ PropertyChanged=�$ TwoWay=���5Y   !   �� *ClassicUserButtonColor+��5Z   !   �� *ClassicUserOption+5^   !   5_      �� 5a       	 Login#���  6   $	��300��5c      $��50��5d      $��DemiBold"�5e      
����p��
�������$��30(�5h      $��Center=�5i      $��0, 50, 0, 0q�5j      $
 Hand��5k      ��5b      �� *LoginCommand+5k      5l      5m      5n      5o      