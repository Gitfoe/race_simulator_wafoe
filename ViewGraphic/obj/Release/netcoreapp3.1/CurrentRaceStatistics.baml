   M S B A M L   `   `   `  ���� O  Krace_simulator_wafoe, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ViewGraphicViewGraphic  clr-namespace:ModelModel  '    !ViewGraphic.CurrentRaceStatistics   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:ViewGraphic     %Modelclr-namespace:Model       � Title$  Current race statistics��$	��450��$	��800�� � DataContext    Model.RaceInfoDataContext  .���  � RowDefinitions �� $��2* ��� $��1* � � ColumnDefinitions �� $��3* ��� $��2* ��� $��2* �.+�� .+��� $
��Left=�$	��Top=�$��Racers:���� $��0,22,0,0q����� *Participants+ �� View �� .P��� $
��Name�� �� DisplayMemberBinding �� *Name+�� $
��Team�� �� *	TeamColor+�� $��Speed�� �� *Equipment.Speed+�� $��Performance�� �� *Equipment.Performance+�� $��Quality�� �� *Equipment.Quality+�� $��IsBroken�� �� *Equipment.IsBroken+� $��1��$��2��.+��� $
��Left=�$	��Top=�$��Positions on track:���� $��0,22,0,0q����� *" ParticipantsDescendingByPosition+ �� .P��� $��Position�� �� *PositionOnTrack+�� $
��Name�� �� *Name+� $��1��.+��� $��Name of track:��$
��Left=�$	��Top=�$
��Bold"�$��0,00,0,0q��� $
��Left=�$	��Top=�$
��Bold"�$��	120,0,0,0q����� *CurrentRace.Track.Name+�� $��Amount of laps:��$
��Left=�$	��Top=�$
��Bold"�$��0,20,0,0q��� $
��Left=�$	��Top=�$
��Bold"�$��
120,20,0,0q����� *AmountOfLaps+�� $��Amount of sections:��$
��Left=�$	��Top=�$
��Bold"�$��0,40,0,0q��� $
��Left=�$	��Top=�$
��Bold"�$��
120,40,0,0q����� *AmountOfSections+�� $��Amount of racers:��$
��Left=�$	��Top=�$
��Bold"�$��0,60,0,0q��� $
��Left=�$	��Top=�$
��Bold"�$��
120,60,0,0q����� *AmountOfRacers+�� $��Race timer:��$
��Left=�$	��Top=�$
��Bold"�$��0,80,0,0q��� $
��Left=�$	��Top=�$
��Bold"�$��
120,80,0,0q����� *	RaceTimer+� $��2��$��2��.+��� $
��Left=�$	��Top=�$��
Lap times:���� $��0,22,0,0q����� *ParticipantLapTimes+ �� .P��� $
��Name�� �� *Name+�� $	��Lap�� �� *Lap+�� $
��Time�� �� *	LapTime+