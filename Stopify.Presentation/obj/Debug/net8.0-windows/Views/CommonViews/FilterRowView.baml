   M S B A M L   `   `   `  ����O  KStopify.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Stopify.Presentation.Views.CommonViews&Stopify.Presentation.Views.CommonViews  5      ]3clr-namespace:Stopify.Presentation.Helpers.Controls%Stopify.Presentation.Helpers.Controls  5      :    4Stopify.Presentation.Views.CommonViews.FilterRowView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:Stopify.Presentation.Views.CommonViews     5      Gcontrol3clr-namespace:Stopify.Presentation.Helpers.Controls     5      .��5	      � $��3��6   $��
15 20 0 20q�6     � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� -   5        $��TitleBtn��5      $��0��5      
������ $��0q�5      
�������$
��Bold"�5      $��24(�5!      ��5       �� *Title+ �� RelativeSource ��  �� AncestorType UserControl5$      �� 5(      -   5        $��
ShowAllBtn��5*      $��2��5)      
������ $��0q�5,      
�������$
��Bold"�5.      $��Show all��5/      $��Bottom=�50      $
��13.5(�51      $��0 0 10 0q�52      55      57      �� 5:   
   $��1��5;       �� Orientation$ 
Horizontal=�5<      $
��0 15q�5=      .+�5@      �� -   5        $��AllBtn��5A      
������ $��0q�5C      $��Stretch=�5D      $��Stretch=�5E      .��5I      �� -   5        $��	AllBorder��5J       �� CornerRadius$ 15��5K      
������� �� Padding$
 13 6q�5M      ._�5N      �� -   5        $��AllText��5O      $	��All��5P      $��SemiBold"�5Q      
���   �$��14(�5S      5U      5V      �� 5Y      -   5        $��MusicBtn��5Z      
������ $��0q�5\      $	��7 0q�5]      $��Stretch=�5^      $��Stretch=�5_      .��5c      �� -   5        $��MusicBorder��5d      $ 15��5e      ���#2a2a2a$
 13 6q�5g      ._�5h      �� -   5        $��	MusicText��5i      $��Music��5j      $��SemiBold"�5k      
�������$��14(�5m      5o      5p      �� 5s      -	   5        $��PodcastsBtn��5t      
������ $��0q�5v      $��Stretch=�5w      $��Stretch=�5x      .��5|      �� -
   5        $��PodcastsBorder��5}      $ 15��5~      ���#2a2a2a$
 13 6q�5�      ._�5�      �� -   5        $��PodcastsText��5�      $��Podcasts��5�      $��SemiBold"�5�      
�������$��14(�5�      5�      5�      5�      =   7Stopify.Presentation.Helpers.Controls.StretchyWrapPanel  5�   
   $��2��6$   .+�5�      ;   5Stopify.Presentation.Views.CommonViews.CommonItemView    5�        5�        5�        5�        5�        5�        5�      5�      5�      5�      