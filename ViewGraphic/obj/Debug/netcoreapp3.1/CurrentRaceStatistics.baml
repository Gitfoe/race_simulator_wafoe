   M S B A M L   `   `   `  ����F  BViewGraphic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ViewGraphicViewGraphic  5   	   clr-namespace:ModelModel  5   	   '    !ViewGraphic.CurrentRaceStatistics   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ViewGraphic     5   	   %Modelclr-namespace:Model     5   	     � Title$  Current race statistics��5	   	   $	��450��6)   $	��800��66    � DataContext 5
         Model.RaceInfoDataContext  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      $��2* �6   �� 5      $��2* �6   .+�5   
   � .+�5      �� $
��Left=�6   $	��Top=�6/   $��Racers:��6G   �� 5      $��0,22,0,0q�6   ��6)   �� *Participants+ �� View 5      �� 5      .P�5      �� $
��Name��6)    �� DisplayMemberBinding 67   �� *Name+�� 5      $
��Team��6)    67   �� *	TeamColor+�� 5      $��Speed��6)    68   �� *Equipment.Speed+�� 5      $��Performance��6)    6>   �� *Equipment.Performance+�� 5       $��Quality��6)    6:   �� *Equipment.Quality+�� 5!      $��IsBroken��6)    6;   �� *Equipment.IsBroken+5"      5$      5%      � 5&   
   $��1��6   $��2��6   .+�5'      �� $
��Left=�6   $	��Top=�6/   $��Positions on track:��6G   �� 5(      $��0,22,0,0q�6   ��6)   �� *" ParticipantsDescendingByPosition+ 5)      �� 5*      .P�5+      �� $��Position��6)    6;   �� *PositionOnTrack+�� 5,      $
��Name��6)    67   �� *Name+5-      5/      50      � 51   
   $��1��6   .+�52      �� $��Name of track:��6   $
��Left=�6-   $	��Top=�6H   $
��Bold"�6`   $��0,00,0,0q�6r   �� 53      $
��Left=�6?   $	��Top=�6Z   $
��Bold"�6r   $��	120,0,0,0q�6�   ��6   �� *CurrentRace.Track.Name+6�   �� 54      $��Amount of laps:��6   $
��Left=�6.   $	��Top=�6I   $
��Bold"�6a   $��0,20,0,0q�6s   �� 55      $
��Left=�65   $	��Top=�6P   $
��Bold"�6h   $��
120,20,0,0q�6z   ��6   �� *AmountOfLaps+6z   �� 56      $��Amount of sections:��6   $
��Left=�62   $	��Top=�6M   $
��Bold"�6e   $��0,40,0,0q�6w   �� 57      $
��Left=�69   $	��Top=�6T   $
��Bold"�6l   $��
120,40,0,0q�6~   ��6   �� *AmountOfSections+6~   �� 58      $��Amount of racers:��6   $
��Left=�60   $	��Top=�6K   $
��Bold"�6c   $��0,60,0,0q�6u   �� 59      $
��Left=�67   $	��Top=�6R   $
��Bold"�6j   $��
120,60,0,0q�6|   ��6   �� *AmountOfRacers+6|   �� 5:      $��Race timer:��6   $
��Left=�6*   $	��Top=�6E   $
��Bold"�6]   $��0,80,0,0q�6o   �� 5;      $
��Left=�62   $	��Top=�6M   $
��Bold"�6e   $��
120,80,0,0q�6w   ��6   �� *	RaceTimer+6w   5<      � 5=   
   $��2��6   $��2��6   .+�5>      �� $
��Left=�6   $	��Top=�6/   $��
Lap times:��6G   �� 5?      $��0,22,0,0q�6   ��6)   �� *ParticipantLapTimes+ 5@      �� 5A      .P�5B      �� $
��Name��6)    67   �� *Name+�� 5C      $	��Lap��6)    66   �� *Lap+�� 5D      $
��Time��6)    67   �� *	LapTime+5E      5G      5H      5I      5J      