   M S B A M L   `   `   `  ����F  BViewGraphic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ViewGraphicViewGraphic  5   	   clr-namespace:ModelModel  5   	   '    !ViewGraphic.CompetitionStatistics   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ViewGraphic     5   	   %Modelclr-namespace:Model     5   	     � Title$  Competition statistics��5	   	   $	��450��6(   $	��800��65    � DataContext 5
      &    Model.CompetitionInfoDataContext  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   � $��2��6   .+�5      �� $
��Left=�6   $	��Top=�6/   $��Total race times:��6G   �� 5      $��0,22,0,0q�6   ��6)   �� *ParticipantRaceTimes+ �� View 5      �� 5      .P�5      �� $
��Name��6)    �� DisplayMemberBinding 67   �� *Name+�� 5      $��Track��6)    68   �� *
Track.Name+�� 5      $
��Time��6)    67   �� *
RaceTime+5      5       5!      � 5"   
   $��1��6   .+�5#      �� $
��Left=�6   $	��Top=�6/   $0��*Total points achieved by racers as of now:��6G   �� 5$      $��0,22,0,0q�6   ��6)   �� *Participants+ 5%      �� 5&      .P�5'      �� $��Position��6)    6;   �� *PositionInCompetition+�� 5(      $
��Name��6)    67   �� *Name+�� 5)      $��Points��6)    69   �� *Points+5*      5,      5-      � 5.   
   $��1��6   $��1��6   .+�5/      �� $
��Left=�6   $	��Top=�6/   $��Tracks in queue:��6G   �� 50      $��0,22,0,0q�6   ��6)   �� *Tracks+ 51      �� 52      .P�53      �� $
��Name��6)    67   �� *Name+�� 54      $
��Laps��6)    67   �� *AmountOfLaps+�� 55      $��Sections��6)    6;   �� *Sections.Count+56      58      59      5:      5;      