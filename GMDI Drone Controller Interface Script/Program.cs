using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {


        #region mdk preserve
        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update100;
        }
        //change this
        string drone_tag = "SWRM_D";
        int scnpanel = 0; //display surface: 0 = default, 0+ range

        #endregion
        //statics
        string confirmval = "confirm";
        string increase = "incrval";
        string decrease = "decrval";
        string incrsel = "incrsel";
        string itemdown = "itemdown";
        string itemup = "itemup";
        string menureturn = "menu";
        string command = "command";
        string jobconf = "jobconf";
        string cancelcommand = "cancel";
        
        string ver = "V0.320A";
        string comms = "Comms";
        string intfs = "Interface";
        string postfix = "Display";
        string drone_controller_tag = "";
        string display_main_tag = "";
        string secondary = "";
        string secondary_tag = "";
        string ant_tg = "";
        int menu_level = 0;
        int item_min_limit = 0;
        int item_max_limit = 9;
        int item_number = 0;
        bool item_up = false;
        bool item_down = false;
        bool data_valid = false;
        IMyProgrammableBlock controller_actual;
        string last_command = "";
        string customData1;
        string customData2;
        string customData3;
        string customData4;
        string customData5;
        string customData6;
        string customData7;
        string customData8;
        string customData9;
        string customData10;
        string customData11;
        string customData12;
        string customData13;
        string customData14;
        string customData15;
        string customData16;
        string customData17;
        string customData18;
        string customData19;
        string customData20;
        string customData21;
        string customData22;
        double ignore_depth = 0.0;
        bool limit_flight_drones = false;
        bool limit_coreout = false;
        int flight_factor = 1;
        int hard_drone_limit = 10;
        double gridsize;
        int numPointsY;
        int numPointsX;
        Vector3D main_gps_coords;
        double drillshaft_length;
        int skipbores;
        string command_dist;
        string iteration_view;
        string limit_display;
        string core_display;
        string cancel_display;
        string menu_display;
        double temp_drillshaft_length = 0.0;
        double temp_ignore_depth = 0.0;
        double temp_gridsize = 0.0;
        int temp_numPointsY = 0;
        int temp_numPointsX = 0;
        int temp_limit_flight_drones = 0;
        int temp_flight_factor = 0;
        int temp_hard_drone_limit = 0;
        int temp_skipbores;
        int temp_limit_coreout;
        int temp_cancel = 0;
        int temp_align_data_keep = 0;
        int temp_menu = 0;
        bool confirm_send = false;
        bool confirm_command = false;
        bool confirm_sel_2 = false;
        bool confirm_sel_1 = false;
        string disp_command = "";
        string displayconfirm_1;
        string displayconfirm_2;
        int iteration_val = 0;
        bool has_iterated = false;
        bool has_increased = false;
        bool has_decreased = true;
        double new_drillshaft_length;
        double new_ignore_depth;
        double new_gridsize;
        int new_skipbores;
        int new_numPointsY;
        int new_numPointsX;
        int new_flight_factor;
        int new_hard_drone_limit;
        bool new_limit_flight_drones;
        bool new_limit_coreout;
        int read_limit_flight_drones = 0;
        int read_limit_coreout = 0;
        int new_int_limit_flight_drones = 0;
        int new_int_limit_coreout = 0;
        int temp_confirmval_1 = 0;
        int temp_confirmval_2 = 0;
        double surfaceDistance = 0.0;
        List<string> item_line_0;
        List<string> item_line_1;
        List<string> item_line_2;
        List<string> item_line_3;
        List<string> item_line_4;
        List<string> item_line_5;
        List<string> item_line_6;
        List<string> item_line_7;
        List<string> item_line_8;
        List<string> item_line_9;
        List<string> item_line_10;
        List<string> item_line_11;
        List<string> item_line_12;
        List<string> scroll_command_item;
        int scroll_item_val = 0;
        int scroll_item_val_min_limit = 0;
        int scroll_item_val_max_limit = 7;
        string line_highlight_0 = "[ ]";
        string line_highlight_1 = "[ ]";
        string line_highlight_2 = "[ ]";
        string line_highlight_3 = "[ ]";
        string line_highlight_4 = "[ ]";
        string line_highlight_5 = "[ ]";
        string line_highlight_6 = "[ ]";
        string line_highlight_7 = "[ ]";
        string line_highlight_8 = "[ ]";
        string line_highlight_9 = "[ ]";
        string line_highlight_10 = "[ ]";
        string line_highlight_11 = "[ ]";
        string line_highlight_12 = "[ ]";
        string icon = "";
        string temp_id_name;
        string temp_id_name_2;
        int stateshift = 0;
        bool setup_complete = false;
        StringBuilder display_view;
        StringBuilder mcd_new;
        IMyTerminalBlock display_actual;
        List<IMyTerminalBlock> display_all;
        List<IMyTerminalBlock> display_tag_main;
        List<IMyProgrammableBlock> program_blocks_all;
        List<IMyProgrammableBlock> program_blocks_tag;
        List<IMyRadioAntenna> at_all;
        List<IMyRadioAntenna> at_tg;
        Vector3D alignGPSCoordinates;
        bool customDataAlignTargetValid;
        string align_display = "";
        


        public void Save()
        {
        }

        public void Main(string argument, UpdateType updateSource)
        {
            IMyGridTerminalSystem gts = GridTerminalSystem as IMyGridTerminalSystem;
            if (!setup_complete)
            {
                Setup(gts);
            }


            if (display_tag_main.Count <= 0 || display_tag_main[0] == null)
            {
                Echo($"Main Displays with tag '{display_main_tag}' not found");
                setup_complete = false;
                return;
            }
            display_actual = display_tag_main[0];
            IMyTextSurface surface = ((IMyTextSurfaceProvider)display_actual).GetSurface(scnpanel);
            if (surface != null)
            {
                if (surface.ContentType != ContentType.TEXT_AND_IMAGE)
                {
                    surface.ContentType = ContentType.TEXT_AND_IMAGE;
                }
            }
            if (surface == null)
            {
                Echo($"Panel:'{scnpanel}' on '{display_main_tag}' not found");
                setup_complete = false;
                return;
            }
            if (program_blocks_tag.Count <= 0 || program_blocks_tag[0] == null)
            {
                Echo($"Drone controller with with tag '{drone_controller_tag}' not found");
                setup_complete = false;
                return;
            }
            if (!setup_complete)
            {
                Echo($"Setup not complete");
                return;
            }
            controller_actual = program_blocks_tag[0];
            Echo($"GMDI {ver} Running {icon}");
            Echo("");
            Echo("Use the below run arguments to navigate:");
            Echo("----------------------------------------");
            Echo("");
            Echo($"Confirm = {confirmval}");
            Echo($"Change increment = {incrsel}");
            Echo($"Increase value = {increase}");
            Echo($"Decrease value = {decrease}");
            Echo($"Main menu = {menureturn}");
            //logic start
            
            GetCustomData();
            state_shifter();
            if (limit_flight_drones)
            {
                read_limit_flight_drones = 1;
            }
            else
            {
                read_limit_flight_drones = 0;
            }
            if (limit_coreout)
            {
                read_limit_coreout = 1;
            }
            else
            {
                read_limit_coreout = 0;
            }
            if (argument == "setup" && setup_complete)
            {
                setup_complete = false;
                argument = "";
                Echo("Running setup...");
            }
            state_shifter();
            //menu stuff
            if (argument.Contains(jobconf))
            {
                menu_level = 2;
                item_number = 0;
                iteration_val = 0;
                argument = "";
            }
            if (argument.Contains(command))
            {
                menu_level = 1;
                item_number = 0;
                iteration_val = 0;
                argument = "";
            }
            if (argument.Contains(menureturn))
            {
                menu_level = 0;
                item_number = 0;
                scroll_item_val = 0;
                iteration_val = 0;
                argument = "";
            }
            if (argument.Contains(cancelcommand))
            {
                menu_level = 0;
                item_number = 0;
                iteration_val = 0;
                scroll_item_val = 7;
                Me.CustomData = "";
                last_command = "";
                argument = "";
            }

            if (argument.Contains(itemup)) //item index up
            {
                if (!item_up)
                {
                    incr_item();
                    item_up = true;
                    argument = "";
                }
            }
            if (item_up)
            {
                item_up = false;
            }

            if (argument.Contains(itemdown)) //item index down
            {
                if (!item_down)
                {
                    decr_item();
                    item_down = true;
                    argument = "";
                }
            }
            if (item_down)
            {
                item_down = false;
            }
            if (argument.Contains(incrsel))
            {
                if (menu_level == 0 && !has_iterated)
                {
                    iteration_val = 0;
                    has_iterated = true;
                }
                if (menu_level == 1 && !has_iterated)
                {
                    iteration_val = 0;
                    has_iterated = true;
                }
                if (menu_level == 2)
                {
                    if (!has_iterated && !has_iterated)
                    {
                        iteration_val++;
                        has_iterated = true;
                    }
                    if (iteration_val > 2)
                    {
                        iteration_val = 0;
                        has_iterated = true;
                    }
                }
                argument = "";
            }
            if (has_iterated)
            {
                has_iterated = false;
            }

            if (argument.Contains(increase))
            {
                if (!has_increased)
                {
                    if (iteration_val == 0)
                    {
                        if (menu_level == 0)
                        {
                            incr_item();
                            has_increased = true;
                        }
                        if (menu_level == 1 && !has_increased)
                        {
                            if (item_number == 0)
                            {
                                incr_scoll_command();
                            }
                            if (item_number == 7)
                            {
                                temp_cancel++;
                            }
                            if (item_number == 8)
                            {
                                temp_menu++;
                            }
                            if (item_number == 12)
                            {
                                temp_confirmval_1++;
                            }
                            has_increased = true;
                        }
                        if (menu_level == 2 && !has_increased)
                        {
                            if (item_number == 0)
                            {
                                temp_numPointsX++;
                            }
                            if (item_number == 1)
                            {
                                temp_numPointsY++;
                            }
                            if (item_number == 2)
                            {
                                temp_gridsize = temp_gridsize + 0.1;
                            }
                            if (item_number == 3)
                            {
                                temp_skipbores++;
                            }
                            if (item_number == 4)
                            {
                                temp_drillshaft_length = temp_drillshaft_length + 0.1;
                            }
                            if (item_number == 5)
                            {
                                temp_ignore_depth = temp_ignore_depth + 0.1;
                            }
                            if (item_number == 6)
                            {
                                temp_limit_flight_drones++;
                            }
                            if (item_number == 7)
                            {
                                temp_hard_drone_limit++;
                            }
                            if (item_number == 8)
                            {
                                temp_flight_factor++;
                            }
                            if (item_number == 9)
                            {
                                temp_limit_coreout++;
                            }
                            if (item_number == 10)
                            {
                                temp_align_data_keep++;
                            }
                            if (item_number == 11)
                            {
                                temp_menu++;
                            }
                            if (item_number == 12)
                            {
                                temp_confirmval_2++;
                            }
                            has_increased = true;
                        }
                    }

                    if (iteration_val == 1)
                    {
                        if (menu_level == 2 && !has_increased)
                        {
                            if (item_number == 0)
                            {
                                temp_numPointsX = temp_numPointsX + 5;
                            }
                            if (item_number == 1)
                            {
                                temp_numPointsY = temp_numPointsY + 5;
                            }
                            if (item_number == 2)
                            {
                                temp_gridsize = temp_gridsize + 1.0;
                            }
                            if (item_number == 3)
                            {
                                temp_skipbores = temp_skipbores + 5;
                            }
                            if (item_number == 4)
                            {
                                temp_drillshaft_length = temp_drillshaft_length + 1.0;
                            }
                            if (item_number == 5)
                            {
                                temp_ignore_depth = temp_ignore_depth + 1.0;
                            }
                            if (item_number == 6)
                            {
                                temp_limit_flight_drones = temp_limit_flight_drones + 1;
                            }
                            if (item_number == 7)
                            {
                                temp_hard_drone_limit = temp_hard_drone_limit + 5;
                            }
                            if (item_number == 8)
                            {
                                temp_flight_factor = temp_flight_factor + 5;
                            }
                            if (item_number == 9)
                            {
                                temp_limit_coreout++;
                            }
                            if (item_number == 10)
                            {
                                temp_align_data_keep++;
                            }
                            if (item_number == 11)
                            {
                                temp_menu++;
                            }
                            if (item_number == 12)
                            {
                                temp_confirmval_2++;
                            }
                            has_increased = true;
                        }
                    }
                  
                    if (iteration_val == 2)
                    {
                        if (menu_level == 2 && !has_increased)
                        {
                            if (item_number == 0)
                            {
                                temp_numPointsX = temp_numPointsX + 10;
                            }
                            if (item_number == 1)
                            {
                                temp_numPointsY = temp_numPointsY + 10;
                            }
                            if (item_number == 2)
                            {
                                temp_gridsize = temp_gridsize + 5.0;
                            }
                            if (item_number == 3)
                            {
                                temp_skipbores = temp_skipbores + 10;
                            }
                            if (item_number == 4)
                            {
                                temp_drillshaft_length = temp_drillshaft_length + 10.0;
                            }
                            if (item_number == 5)
                            {
                                temp_ignore_depth = temp_ignore_depth + 10.0;
                            }
                            if (item_number == 6)
                            {
                                temp_limit_flight_drones++;
                            }
                            if (item_number == 7)
                            {
                                temp_hard_drone_limit = temp_hard_drone_limit + 10;
                            }
                            if (item_number == 8)
                            {
                                temp_flight_factor = temp_flight_factor + 10;
                            }
                            if (item_number == 9)
                            {
                                temp_limit_coreout++;
                            }
                            if (item_number == 10)
                            {
                                temp_align_data_keep++;
                            }
                            if (item_number == 11)
                            {
                                temp_menu++;
                            }
                            if (item_number == 12)
                            {
                                temp_confirmval_2++;
                            }
                            has_increased = true;
                        }
                    }

                }
                argument = "";
            }
            if (has_increased)
            {
                has_increased = false;
            }
            
            if (argument.Contains(decrease))
            {

                if (!has_decreased)
                {
                    if (iteration_val == 0)
                    {
                        if (menu_level == 0)
                        {
                            decr_item();
                            has_decreased = true;
                        }
                        if (menu_level == 1 && !has_decreased)
                        {
                            if (item_number == 0)
                            {
                                decr_scoll_command();
                            }
                            if (item_number == 7)
                            {
                                temp_cancel--;
                            }
                            if (item_number == 8)
                            {
                                temp_menu--;
                            }
                            if (item_number == 12)
                            {
                                temp_confirmval_1--;
                            }
                            has_decreased = true;
                        }
                        if (menu_level == 2 && !has_decreased)
                        {
                            if (item_number == 0)
                            {
                                temp_numPointsX--;
                            }
                            if (item_number == 1)
                            {
                                temp_numPointsY--;
                            }
                            if (item_number == 2)
                            {
                                temp_gridsize = temp_gridsize - 0.1;
                            }
                            if (item_number == 3)
                            {
                                temp_skipbores--;
                            }
                            if (item_number == 4)
                            {
                                temp_drillshaft_length = temp_drillshaft_length - 0.1;
                            }
                            if (item_number == 5)
                            {
                                temp_ignore_depth = temp_ignore_depth - 0.1;
                            }
                            if (item_number == 6)
                            {
                                temp_limit_flight_drones--;
                            }
                            if (item_number == 7)
                            {
                                temp_hard_drone_limit--;
                            }
                            if (item_number == 8)
                            {
                                temp_flight_factor--;
                            }
                            if (item_number == 9)
                            {
                                temp_limit_coreout--;
                            }
                            if (item_number == 10)
                            {
                                temp_align_data_keep--;
                            }
                            if (item_number == 11)
                            {
                                temp_menu--;
                            }
                            if (item_number == 12)
                            {
                                temp_confirmval_2--;
                            }
                            has_decreased = true;
                        }
                    }
                    
                    if (iteration_val == 1)
                    {
                        if (menu_level == 2 && !has_decreased)
                        {
                            if (item_number == 0)
                            {
                                temp_numPointsX = temp_numPointsX - 5;
                            }
                            if (item_number == 1)
                            {
                                temp_numPointsY = temp_numPointsY - 5;
                            }
                            if (item_number == 2)
                            {
                                temp_gridsize = temp_gridsize - 1.0;
                            }
                            if (item_number == 3)
                            {
                                temp_skipbores = temp_skipbores - 5;
                            }
                            if (item_number == 4)
                            {
                                temp_drillshaft_length = temp_drillshaft_length - 1.0;
                            }
                            if (item_number == 5)
                            {
                                temp_ignore_depth = temp_ignore_depth - 1.0;
                            }
                            if (item_number == 6)
                            {
                                temp_limit_flight_drones--;
                            }
                            if (item_number == 7)
                            {
                                temp_hard_drone_limit = temp_hard_drone_limit - 5;
                            }
                            if (item_number == 8)
                            {
                                temp_flight_factor = temp_flight_factor - 5;
                            }
                            if (item_number == 9)
                            {
                                temp_limit_coreout--;
                            }
                            if (item_number == 10)
                            {
                                temp_align_data_keep--;
                            }
                            if (item_number == 11)
                            {
                                temp_menu--;
                            }
                            if (item_number == 12)
                            {
                                temp_confirmval_2--;
                            }
                            has_decreased = true;
                        }
                    }
                    
                    if (iteration_val == 2)
                    {
                        if (menu_level == 2 && !has_decreased)
                        {
                            if (item_number == 0)
                            {
                                temp_numPointsX = temp_numPointsX - 10;
                            }
                            if (item_number == 1)
                            {
                                temp_numPointsY = temp_numPointsY - 10;
                            }
                            if (item_number == 2)
                            {
                                temp_gridsize = temp_gridsize - 5.0;
                            }
                            if (item_number == 3)
                            {
                                temp_skipbores = temp_skipbores - 10;
                            }
                            if (item_number == 4)
                            {
                                temp_drillshaft_length = temp_drillshaft_length - 10.0;
                            }
                            if (item_number == 5)
                            {
                                temp_ignore_depth = temp_ignore_depth - 10.0;
                            }
                            if (item_number == 6)
                            {
                                temp_limit_flight_drones--;
                            }
                            if (item_number == 7)
                            {
                                temp_hard_drone_limit = temp_hard_drone_limit - 10;
                            }
                            if (item_number == 8)
                            {
                                temp_flight_factor = temp_flight_factor - 10;
                            }
                            if (item_number == 9)
                            {
                                temp_limit_coreout--;
                            }
                            if (item_number == 10)
                            {
                                temp_align_data_keep--;
                            }
                            if (item_number == 11)
                            {
                                temp_menu--;
                            }
                            if (item_number == 12)
                            {
                                temp_confirmval_2--;
                            }
                            has_decreased = true;
                        }
                    }
                }
                argument = "";
            }
            if (has_decreased)
            {
                has_decreased = false;
            }
            
            if (menu_level == 0)
            {
                if (iteration_val == 0)
                {
                    if (item_number == 0)
                    {
                        iteration_view = "";
                    }
                }
            }
            if (menu_level == 1)
            {
                if (iteration_val == 0)
                {
                    if (item_number == 0)
                    {
                        iteration_view = "1";
                    }
                    if (item_number == 7 || item_number == 8 || item_number == 9 || item_number == 10 || item_number == 11 || item_number == 12)
                    {
                        iteration_view = "Yes/No";
                    }
                }
            }
            if (menu_level == 2)
            {
                if (iteration_val == 0)
                {
                    if (item_number == 0 || item_number == 1 || item_number == 3 || item_number == 7 || item_number == 8)
                    {
                        iteration_view = "1";
                    }
                    if (item_number == 2 || item_number == 4 || item_number == 5)
                    {
                        iteration_view = "0.1";
                    }
                    if (item_number == 6)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 9)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 10)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 11)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 12)
                    {
                        iteration_view = "Yes/No";
                    }
                }
                
                if (iteration_val == 1)
                {
                    if (item_number == 0 || item_number == 1 || item_number == 3 || item_number == 7 || item_number == 8)
                    {
                        iteration_view = "5";
                    }
                    if (item_number == 2 || item_number == 4 || item_number == 5)
                    {
                        iteration_view = "1.0";
                    }
                    if (item_number == 6)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 9)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 10)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 11)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 12)
                    {
                        iteration_view = "Yes/No";
                    }
                }
                
                if (iteration_val == 2)
                {
                    if (item_number == 0 || item_number == 1 || item_number == 3 || item_number == 7 || item_number == 8)
                    {
                        iteration_view = "10";
                    }
                    if (item_number == 2 || item_number == 4 || item_number == 5)
                    {
                        iteration_view = "10.0";
                    }
                    if (item_number == 6)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 9)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 10)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 11)
                    {
                        iteration_view = "Yes/No";
                    }
                    if (item_number == 12)
                    {
                        iteration_view = "Yes/No";
                    }
                }
            }


            
            //new totals for disp
            new_numPointsX = numPointsX + temp_numPointsX;
            if (new_numPointsX < 1)
            {
                new_numPointsX = 1;
            }

            new_numPointsY = numPointsY + temp_numPointsY;
            if (new_numPointsY < 1)
            {
                new_numPointsY = 1;
            }

            new_gridsize = gridsize + temp_gridsize;
            if (new_gridsize < 0.1)
            {
                new_gridsize = 0.1;
            }

            new_drillshaft_length = drillshaft_length + temp_drillshaft_length;
            if (new_drillshaft_length < 0.1)
            {
                new_drillshaft_length = 0.1;
            }

            new_ignore_depth = ignore_depth + temp_ignore_depth;
            //flight drone limit enable manage
            if (temp_limit_flight_drones < 0)
            {
                temp_limit_flight_drones = 1;
            }
            if (temp_limit_flight_drones > 1)
            {
                temp_limit_flight_drones = 0;
            }
            if (temp_limit_flight_drones == 0)
            {
                limit_display = "No";
            }
            if (temp_limit_flight_drones == 1)
            {
                limit_display = "Yes";
            }
            new_int_limit_flight_drones = temp_limit_flight_drones + read_limit_flight_drones;
            if (new_int_limit_flight_drones < 0)
            {
                new_int_limit_flight_drones = 1;
            }
            if (new_int_limit_flight_drones > 1)
            {
                new_int_limit_flight_drones = 0;
            }
            if (new_int_limit_flight_drones == 0)
            {
                new_limit_flight_drones = false;
            }
            if (new_int_limit_flight_drones == 1)
            {
                new_limit_flight_drones = true;
            }
            if (new_int_limit_flight_drones == 0)
            {
                limit_display = "No";
            }
            if (new_int_limit_flight_drones == 1)
            {
                limit_display = "Yes";
            }

            new_hard_drone_limit = hard_drone_limit + temp_hard_drone_limit;
            if (new_hard_drone_limit < 1)
            {
                new_hard_drone_limit = 1;
            }

            new_flight_factor = flight_factor + temp_flight_factor;
            if (new_flight_factor < 0)
            {
                new_flight_factor = 0;
            }


            new_skipbores = skipbores + temp_skipbores;
            if (new_skipbores < 0)
            {
                new_skipbores = 0;
            }
            //coreout management
            if (temp_limit_coreout < 0)
            {
                temp_limit_coreout = 1;
            }
            if (temp_limit_coreout > 1)
            {
                temp_limit_coreout = 0;
            }
            if (temp_limit_coreout == 0)
            {
                core_display = "No";
            }
            if (temp_limit_coreout == 1)
            {
                core_display = "Yes";
            }
            new_int_limit_coreout = temp_limit_coreout + read_limit_coreout;
            if (new_int_limit_coreout < 0)
            {
                new_int_limit_coreout = 1;
            }
            if (new_int_limit_coreout > 1)
            {
                new_int_limit_coreout = 0;
            }
            if (new_int_limit_coreout == 0)
            {
                new_limit_coreout = false;
            }
            if (new_int_limit_coreout == 1)
            {
                new_limit_coreout = true;
            }
            if (new_int_limit_coreout == 0)
            {
                core_display = "No";
            }
            if (new_int_limit_coreout == 1)
            {
                core_display = "Yes";
            }
            //cancel management
            if (temp_cancel < 0)
            {
                temp_cancel = 1;
            }
            if (temp_cancel > 1)
            {
                temp_cancel = 0;
            }
            if (temp_cancel == 0)
            {
                cancel_display = "No";
            }
            if (temp_cancel == 1)
            {
                cancel_display = "Yes";
            }
            //menu management
            if (temp_menu < 0)
            {
                temp_menu = 1;
            }
            if (temp_menu > 1)
            {
                temp_menu = 0;
            }
            if (temp_menu == 0)
            {
                menu_display = "No";
            }
            if (temp_menu == 1)
            {
                menu_display = "Yes";
            }
            //temp align management
            if (temp_align_data_keep < 0)
            {
                temp_align_data_keep = 1;
            }
            if (temp_align_data_keep > 1)
            {
                temp_align_data_keep = 0;
            }
            if (temp_align_data_keep == 0)
            {
                align_display = "No";
            }
            if (temp_align_data_keep == 1)
            {
                align_display = "Yes";
            }

            //confirm management
            if (temp_confirmval_1 < 0)
            {
                temp_confirmval_1 = 1;
            }
            if (temp_confirmval_1 > 1)
            {
                temp_confirmval_1 = 0;
            }
            if (temp_confirmval_1 == 1)
            {
                confirm_sel_1 = true;
            }
            if (temp_confirmval_1 == 0)
            {
                confirm_sel_1 = false;
            }

            if (temp_confirmval_2 < 0)
            {
                temp_confirmval_2 = 1;
            }
            if (temp_confirmval_2 > 1)
            {
                temp_confirmval_2 = 0;
            }
            if (temp_confirmval_2 == 1)
            {
                confirm_sel_2 = true;
            }
            if (temp_confirmval_2 == 0)
            {
                confirm_sel_2 = false;
            }


            //confirm display
            if (confirm_sel_1)
            {
                displayconfirm_1 = "Yes";
            }
            if (!confirm_sel_1)
            {
                displayconfirm_1 = "No";
            }
            if (confirm_sel_2)
            {
                displayconfirm_2 = "Yes";
            }
            if (!confirm_sel_2)
            {
                displayconfirm_2 = "No";
            }
            
            // confirm management
            if (argument.Contains("confirm"))
            {
                if (menu_level == 0)
                {
                    if (item_number == 0)
                    {
                        menu_level = 2;
                        item_number = 0;
                        argument = "";
                    }
                }
            }
            if (argument.Contains("confirm"))
            {
                if (menu_level == 0)
                {
                    if (item_number == 1)
                    {
                        menu_level = 1;
                        item_number = 0;
                        argument = "";
                    }
                }
            }
            if (argument.Contains("confirm"))
            {
                if (menu_level == 1)
                {
                    if (item_number == 12 && !confirm_sel_1)
                    {
                        item_number = 0;
                        confirm_command = false;
                        argument = "";
                    }
                    if (item_number == 12 && confirm_sel_1)
                    {
                        if (temp_cancel == 1)
                        {
                            scroll_item_val = 7;                            
                            temp_menu = 0;
                        }
                        if (temp_menu == 0)
                        {
                            command_resolver();
                            Me.CustomData = disp_command;
                            last_command = disp_command;
                            confirm_command = true;
                            item_number = 0;
                            scroll_item_val = 0;
                            temp_confirmval_1 = 0;
                            temp_cancel = 0;
                            temp_align_data_keep = 0;
                            confirm_sel_1 = false;
                            argument = "";
                        }
                        if (temp_menu == 1)
                        {
                            confirm_command = true;
                            menu_level = 0;
                            item_number = 0;
                            scroll_item_val = 0;
                            temp_confirmval_1 = 0;
                            temp_cancel = 0;
                            temp_menu = 0;
                            temp_align_data_keep = 0;
                            confirm_sel_1 = false;
                            iteration_val = 0;
                            argument = "";
                        }
                    }
                }
            }
            if (argument.Contains("confirm"))
            {
                //if menu is job configuration
                if (menu_level == 2)
                {
                    if (item_number == 12 && !confirm_sel_2)
                    {
                        incr_item();
                        confirm_send = false;
                        argument = "";
                    }
                    if (item_number == 12 && confirm_sel_2)
                    {
                        if (temp_menu == 0)
                        {
                            //send data to program block custom data                    
                            if (data_valid)
                            {
                                mcd_new.Clear();
                                mcd_new.Append("GPS");
                                mcd_new.Append(":");
                                mcd_new.Append("DDT");
                                mcd_new.Append(":");
                                mcd_new.Append(Math.Round(main_gps_coords.X, 2));
                                mcd_new.Append(":");
                                mcd_new.Append(Math.Round(main_gps_coords.Y, 2));
                                mcd_new.Append(":");
                                mcd_new.Append(Math.Round(main_gps_coords.Z, 2));
                                mcd_new.Append(":");
                                mcd_new.Append("#FF75C9F1");
                                mcd_new.Append(":");
                                mcd_new.Append(new_drillshaft_length);
                                mcd_new.Append(":");
                                mcd_new.Append(new_gridsize);
                                mcd_new.Append(":");
                                mcd_new.Append(new_numPointsX);
                                mcd_new.Append(":");
                                mcd_new.Append(new_numPointsY);
                                mcd_new.Append(":");
                                mcd_new.Append(new_ignore_depth);
                                mcd_new.Append(":");
                                mcd_new.Append(new_limit_flight_drones);
                                mcd_new.Append(":");
                                mcd_new.Append(new_flight_factor);
                                mcd_new.Append(":");
                                mcd_new.Append(new_hard_drone_limit);
                                mcd_new.Append(":");
                                mcd_new.Append(new_skipbores);
                                mcd_new.Append(":");
                                mcd_new.Append(new_limit_coreout);
                                mcd_new.Append(":");
                                if (customDataAlignTargetValid && temp_align_data_keep==1)
                                {
                                    mcd_new.Append($"GPS:DDT:{alignGPSCoordinates.X}:{alignGPSCoordinates.Y}:{alignGPSCoordinates.Z}:#FF75C9F1:{surfaceDistance}:");
                                }
                            }
                            controller_actual.CustomData = mcd_new.ToString();
                            confirm_send = true;
                            temp_drillshaft_length = 0.0;
                            temp_ignore_depth = 0.0;
                            temp_gridsize = 0.0;
                            temp_flight_factor = 0;
                            temp_hard_drone_limit = 0;
                            temp_numPointsX = 0;
                            temp_numPointsY = 0;
                            temp_skipbores = 0;
                            temp_limit_flight_drones = 0;
                            temp_limit_coreout = 0;
                            temp_menu = 0;
                            temp_align_data_keep = 0;
                            temp_confirmval_2 = 0;
                            confirm_sel_2 = false;
                            incr_item();
                            argument = "";
                        }
                        if(temp_menu == 1)
                        {
                            menu_level = 0;
                            iteration_val = 0;
                            item_number = 0;
                            confirm_send = true;
                            temp_drillshaft_length = 0.0;
                            temp_ignore_depth = 0.0;
                            temp_gridsize = 0.0;
                            temp_flight_factor = 0;
                            temp_hard_drone_limit = 0;
                            temp_numPointsX = 0;
                            temp_numPointsY = 0;
                            temp_skipbores = 0;
                            temp_limit_flight_drones = 0;
                            temp_limit_coreout = 0;
                            temp_menu = 0;
                            temp_align_data_keep = 0;
                            temp_confirmval_2 = 0;
                            confirm_sel_2 = false;                            
                            argument = "";
                        }
                    }
                }
            }

            
            if (argument.Contains("confirm"))
            {
                // if menu is command configuration
                if (menu_level == 1)
                {
                    if (item_number == 0)
                    {
                        item_number = 7;
                        argument = "";
                    }
                }
            }
            if (argument.Contains("confirm"))
            {
                // if menu is command configuration
                if (menu_level == 1)
                {
                    if (item_number == 7)
                    {
                        item_number = 8;
                        argument = "";
                    }
                }
            }
            if (argument.Contains("confirm"))
            {
                if (menu_level == 1)
                {
                    if (item_number == 8)
                    {
                        item_number = 12;
                        argument = "";
                    }
                }
            }
            if (argument.Contains("confirm"))
            {
                if (menu_level == 2)
                {
                    if (item_number >= 0 && item_number <= 12)
                    {
                        incr_item();
                        argument = "";
                    }
                }
            }
            if (setup_complete)
            {
                LineResolver(item_number);
                screen_display();
                if (display_tag_main.Count > 0 && display_actual != null)
                {
                    surface.WriteText(display_view.ToString());
                }
            }

            if (confirm_send)
            {
                confirm_send = false;
            }
            if (confirm_command)
            {
                confirm_command = false;
            }
            
        }

        private void Setup(IMyGridTerminalSystem gts)
        {
            drone_controller_tag = "[" + drone_tag + " " + comms + "]";
            display_main_tag = "[" + drone_tag + " " + intfs + " " + postfix + "]";
            ant_tg = "[" + drone_tag + " " + comms + "]";
            secondary_tag = $"[{secondary}]";
            item_line_0 = new List<string>();
            item_line_1 = new List<string>();
            item_line_2 = new List<string>();
            item_line_3 = new List<string>();
            item_line_4 = new List<string>();
            item_line_5 = new List<string>();
            item_line_6 = new List<string>();
            item_line_7 = new List<string>();
            item_line_8 = new List<string>();
            item_line_9 = new List<string>();
            item_line_10 = new List<string>();
            item_line_11 = new List<string>();
            item_line_12 = new List<string>();
            scroll_command_item = new List<string>();
            display_view = new StringBuilder();
            mcd_new = new StringBuilder();
            //scroll command item text                
            scroll_command_item.Add("Initialize mining grid");
            scroll_command_item.Add("Reset drones");
            scroll_command_item.Add("Run mining job");
            scroll_command_item.Add("Recall drones to dock");
            scroll_command_item.Add("Undock drones");
            scroll_command_item.Add("Freeze command (dev)");
            scroll_command_item.Add("Stop command (dev)");
            scroll_command_item.Add("");

            //menu text - level 0
            item_line_0.Add("Mining Job Configuration");
            item_line_1.Add("Command Menu");
            item_line_2.Add("");
            item_line_3.Add("");
            item_line_4.Add("");
            item_line_5.Add("");
            item_line_6.Add("");
            item_line_7.Add("");
            item_line_8.Add("");
            item_line_9.Add("");
            item_line_10.Add("");
            item_line_11.Add("");
            item_line_12.Add("");

            //menu text - level 1
            item_line_0.Add("Command:");
            item_line_1.Add("---");
            item_line_2.Add("---");
            item_line_3.Add("---");
            item_line_4.Add("---");
            item_line_5.Add("---");
            item_line_6.Add("---");
            item_line_7.Add("Cancel:");
            item_line_8.Add("Main Menu:");
            item_line_9.Add("---");
            item_line_10.Add("---");
            item_line_11.Add("---:");
            item_line_12.Add("Confirm:");

            //menu text - level 2
            item_line_0.Add("Number Grid X positions:");
            item_line_1.Add("Number Grid Y positions:");
            item_line_2.Add("Grid Spread:");
            item_line_3.Add("Skip Bores:");
            item_line_4.Add("Drill Depth:");
            item_line_5.Add("Ignore Depth:");
            item_line_6.Add("Limit drones in-flight:");
            item_line_7.Add("In-Flight Hard Limit:");
            item_line_8.Add("In-Flight Factor:");
            item_line_9.Add("Core out:");
            item_line_10.Add("Align data:");
            item_line_11.Add("Main Menu:");
            item_line_12.Add("Confirm:");
            menu_level = 0;
            item_number = 0;
            Me.CustomData = "";
            at_all = new List<IMyRadioAntenna>();
            at_tg = new List<IMyRadioAntenna>();
            gts.GetBlocksOfType<IMyRadioAntenna>(at_all, b => b.CubeGrid == Me.CubeGrid);
            for (int i = 0; i < at_all.Count; i++)
            {
                if (at_all[i].CustomName.Contains(comms))
                {
                    string checker = at_all[i].CustomData;
                    drone_custom_data_check(checker, i);
                    if (drone_tag == "" || drone_tag == null)
                    {
                        Echo($"Invalid name for drone_tag {drone_tag}. please add vailid drone tag (drone group name) to antenna custom data e.g. 'SWRM_D:Atlas:', '<drone_tag>:<ship_name>:");
                        return;
                    }
                    at_tg.Add(at_all[i]);
                }
            }
            at_all.Clear();
            display_all = new List<IMyTerminalBlock>();
            display_tag_main = new List<IMyTerminalBlock>();
            gts.GetBlocksOfType<IMyTerminalBlock>(display_all);
            for (int i = 0; i < display_all.Count; i++)
            {
                if (display_all[i].CustomName.Contains(display_main_tag))
                {
                    display_tag_main.Add(display_all[i]);
                }
            }
            display_all.Clear();
            program_blocks_all = new List<IMyProgrammableBlock>();
            program_blocks_tag = new List<IMyProgrammableBlock>();
            gts.GetBlocksOfType<IMyProgrammableBlock>(program_blocks_all);
            for (int i = 0; i < program_blocks_all.Count; i++)
            {
                if (program_blocks_all[i].CustomName.Contains(drone_controller_tag))
                {
                    program_blocks_tag.Add(program_blocks_all[i]);
                }
            }
            program_blocks_all.Clear();
            setup_complete = true;
            Echo("Setup complete!");
        }

        void GetCustomData()
        {
            // get custom data from programmable block
            String[] gpsCommand = controller_actual.CustomData.Split(':');

            //Define GPS coordinates from 
            if (gpsCommand.Length < 10)
            {
                customData1 = "";
                customData2 = "";
                customData3 = "";
                customData4 = "";
                customData5 = "";
                customData6 = "";
                customData7 = "";
                customData8 = "";
                customData9 = "";
                customData10 = "";
                customData11 = "";
                customData12 = "";
                customData13 = "";
                customData14 = "";
                customData15 = "";
                customData16 = "";
                customData17 = "";
                customData18 = "";
                customData19 = "";
                customData20 = "";
                customData21 = "";
                customData22 = "";
                Echo("Please use prospector to assign a mining location");
                data_valid = false;
                return;
            }
            else
            {
                data_valid = true;
            }
            if (gpsCommand.Length > 4)
            {
                main_gps_coords = new Vector3D(Double.Parse(gpsCommand[2]), Double.Parse(gpsCommand[3]), Double.Parse(gpsCommand[4]));
                customData1 = gpsCommand[1];
                customData2 = gpsCommand[2];
                customData3 = gpsCommand[3];
                customData4 = gpsCommand[4];
                customData5 = gpsCommand[5];
            }

            if (gpsCommand.Length < 6)
            {
                customData6 = "";
                drillshaft_length = 1.0;
                customData7 = "";
                gridsize = 0.0;
                customData8 = "";
                numPointsX = 0;
                customData9 = "";
                numPointsY = 0;
                customData10 = "";
                ignore_depth = 0.0;
                customData11 = "";
                limit_flight_drones = false;
                customData12 = "";
                flight_factor = 1;
                customData13 = "";
                hard_drone_limit = 6;
                customData14 = "";
                skipbores = 0;
                customData15 = "";
                limit_coreout = false;
                //coreout = 0;
                return;
            }
            if (gpsCommand.Length > 5)
            {
                if (gpsCommand.Length > 5)
                {
                    customData6 = gpsCommand[6];
                    command_dist = customData6;
                    if (Double.TryParse(command_dist, out drillshaft_length))
                    {
                        Double.TryParse(command_dist, out drillshaft_length);
                    }
                    else
                    {
                        drillshaft_length = 1.0;
                    }


                }
                else
                {
                    customData6 = "";
                    drillshaft_length = 1.0;
                }
            }

            if (gpsCommand.Length < 7)
            {
                customData7 = "";
                gridsize = 0.0;
                customData8 = "";
                numPointsX = 0;
                customData9 = "";
                numPointsY = 0;
                customData10 = "";
                ignore_depth = 0.0;
                customData11 = "";
                limit_flight_drones = false;
                customData12 = "";
                flight_factor = 1;
                customData13 = "";
                hard_drone_limit = 6;
                customData14 = "";
                skipbores = 0;
                customData15 = "";
                limit_coreout = false;
                //coreout = 0;
                return;
            }

            if (gpsCommand.Length > 6)
            {
                customData7 = gpsCommand[7];
                if (double.TryParse(customData7, out gridsize))
                {
                    double.TryParse(customData7, out gridsize);
                }
                else
                {
                    gridsize = 0.0;
                }
            }
            else
            {
                customData7 = "";
                gridsize = 0.0;
            }

            if (gpsCommand.Length < 8)
            {
                customData7 = "";
                gridsize = 0.0;
                customData8 = "";
                numPointsX = 0;
                customData9 = "";
                numPointsY = 0;
                customData10 = "";
                ignore_depth = 0.0;
                customData11 = "";
                limit_flight_drones = false;
                customData12 = "";
                flight_factor = 1;
                customData13 = "";
                hard_drone_limit = 6;
                customData14 = "";
                skipbores = 0;
                customData15 = "";
                limit_coreout = false;
                //coreout = 0;
                return;
            }
            if (gpsCommand.Length > 7)
            {
                customData8 = gpsCommand[8];
                if (int.TryParse(customData8, out numPointsX))
                {
                    int.TryParse(customData8, out numPointsX);
                }
                else
                {
                    numPointsX = 0;
                }
            }
            else
            {
                customData8 = "";
                numPointsX = 0;
            }

            if (gpsCommand.Length < 9)
            {
                customData7 = "";
                gridsize = 0.0;
                customData8 = "";
                numPointsX = 0;
                customData9 = "";
                numPointsY = 0;
                customData10 = "";
                ignore_depth = 0.0;
                customData11 = "";
                limit_flight_drones = false;
                customData12 = "";
                flight_factor = 1;
                customData13 = "";
                hard_drone_limit = 6;
                customData14 = "";
                skipbores = 0;
                customData15 = "";
                limit_coreout = false;
                //coreout = 0;
                return;
            }
            if (gpsCommand.Length > 8)
            {
                customData9 = gpsCommand[9];

                if (int.TryParse(customData9, out numPointsY))
                {
                    int.TryParse(customData9, out numPointsY);
                }
                else
                {
                    numPointsY = 0;
                }
            }
            else
            {
                customData9 = "";
                numPointsY = 0;
            }


            if (gpsCommand.Length < 10)
            {
                customData10 = "";
                ignore_depth = 0.0;
                customData11 = "";
                limit_flight_drones = false;
                customData12 = "";
                flight_factor = 1;
                customData13 = "";
                hard_drone_limit = 6;
                customData14 = "";
                skipbores = 0;
                customData15 = "";
                limit_coreout = false;
                //coreout = 0;
                return;
            }
            if (gpsCommand.Length > 9)
            {
                customData10 = gpsCommand[10];
                if (Double.TryParse(customData10, out ignore_depth))
                {
                    Double.TryParse(customData10, out ignore_depth);
                }
                else
                {
                    ignore_depth = 0.0;
                }
            }
            else
            {
                customData10 = "";
                ignore_depth = 0.0;
            }

            if (gpsCommand.Length < 12)
            {
                customData11 = "";
                limit_flight_drones = false;
                customData12 = "";
                flight_factor = 1;
                customData13 = "";
                hard_drone_limit = 6;
                customData14 = "";
                skipbores = 0;
                customData15 = "";
                limit_coreout = false;
                //coreout = 0;
                return;
            }
            if (gpsCommand.Length > 11)
            {
                customData11 = gpsCommand[11];
                if (bool.TryParse(customData11, out limit_flight_drones))
                {
                    bool.TryParse(customData11, out limit_flight_drones);
                }
                else
                {
                    limit_flight_drones = false;
                }
            }
            else
            {
                customData11 = "";
                limit_flight_drones = false;
            }

            if (gpsCommand.Length < 13)
            {
                customData12 = "";
                flight_factor = 1;
                customData13 = "";
                hard_drone_limit = 6;
                customData14 = "";
                skipbores = 0;
                customData15 = "";
                limit_coreout = false;
                //coreout = 0;
                return;
            }
            if (gpsCommand.Length > 12)
            {
                customData12 = gpsCommand[12];
                if (int.TryParse(customData12, out flight_factor))
                {
                    int.TryParse(customData12, out flight_factor);
                }
                else
                {
                    flight_factor = 1;
                }
            }
            else
            {
                customData12 = "";
                flight_factor = 1;
            }

            if (gpsCommand.Length < 14)
            {
                customData13 = "";
                hard_drone_limit = 6;
                customData14 = "";
                skipbores = 0;
                customData15 = "";
                limit_coreout = false;
                //coreout = 0;
                return;
            }

            if (gpsCommand.Length > 13)
            {
                customData13 = gpsCommand[13];
                if (int.TryParse(customData13, out hard_drone_limit))
                {
                    int.TryParse(customData13, out hard_drone_limit);
                }
                else
                {
                    hard_drone_limit = 6;
                }
            }
            else
            {
                customData13 = "";
                hard_drone_limit = 6;
            }
            if (gpsCommand.Length < 15)
            {
                customData14 = "";
                skipbores = 0;
                customData15 = "";
                limit_coreout = false;
                return;
            }

            if (gpsCommand.Length > 14)
            {
                customData14 = gpsCommand[14];
                if (int.TryParse(customData14, out skipbores))
                {
                    int.TryParse(customData14, out skipbores);
                    data_valid = true;
                }
                else
                {
                    skipbores = 0;
                }
            }
            else
            {
                customData14 = "";
                skipbores = 0;
            }
            if (gpsCommand.Length < 16)
            {
                customData15 = "";
                limit_coreout = false;
                //coreout = 0;
                return;
            }
            if (gpsCommand.Length > 15)
            {
                customData15 = gpsCommand[15];
                if (bool.TryParse(customData15, out limit_coreout))
                {
                    bool.TryParse(customData15, out limit_coreout);
                    data_valid = true;
                }
                else
                {
                    limit_coreout = false;
                }
            }
            else
            {
                customData15 = "";
                limit_coreout = false;
            }
            if (gpsCommand.Length > 16)
            {
                Echo($"gpsCommandLen:{gpsCommand.Length}");
                bool targetAlignX;
                bool targetAlignY;
                bool targetAlignZ;
                if (gpsCommand.Length > 16)
                {
                    customData16 = gpsCommand[16];
                }
                if (gpsCommand.Length > 17)
                {
                    customData17 = gpsCommand[17];
                }
                if (gpsCommand.Length > 18)
                {
                    customData18 = gpsCommand[18];
                }
                if (gpsCommand.Length > 19)
                {
                    customData19 = gpsCommand[19];
                }
                if (gpsCommand.Length > 20)
                {
                    customData20 = gpsCommand[20];
                }
                if (gpsCommand.Length > 21)
                {
                    customData21 = gpsCommand[21];
                }
                if (gpsCommand.Length > 22)
                {
                    customData22 = gpsCommand[22];
                }


                if (!double.TryParse(customData18, out alignGPSCoordinates.X))
                {
                    alignGPSCoordinates.X = 0.0;
                    customData18 = "";
                    targetAlignX = false;
                }
                else
                {
                    targetAlignX = true;
                }
                if (!double.TryParse(customData19, out alignGPSCoordinates.Y))
                {
                    alignGPSCoordinates.Y = 0.0;
                    customData19 = "";
                    targetAlignY = false;
                }
                else
                {
                    targetAlignY = true;
                }
                if (!double.TryParse(customData20, out alignGPSCoordinates.Z))
                {
                    alignGPSCoordinates.Z = 0.0;
                    customData20 = "";
                    targetAlignZ = false;
                }
                else
                {
                    targetAlignZ = true;
                }
                if (targetAlignX && targetAlignY && targetAlignZ)
                {
                    customDataAlignTargetValid = true;
                }
                else
                {
                    customDataAlignTargetValid = false;
                }
                if (gpsCommand.Length > 22)
                {
                    if (!double.TryParse(customData22, out surfaceDistance))
                    {
                        surfaceDistance = 30.0;
                        customData22 = "";
                    }
                }
                if (customDataAlignTargetValid && gpsCommand.Length > 16 && gpsCommand.Length < 18)
                {
                    customDataAlignTargetValid = false;
                    // Me.CustomData = tempbro + $"GPS:TGT:{alignGPSCoordinates.X}:{alignGPSCoordinates.Y}:{alignGPSCoordinates.Z}:#F77668:";
                }

            }
        }

        public void LineResolver(int linevalin)
        {
            if (linevalin == 0)
            {
                line_highlight_0 = "[O]";
            }
            else
            {
                line_highlight_0 = "[ ]";
            }
            if (linevalin == 1)
            {
                line_highlight_1 = "[O]";
            }
            else
            {
                line_highlight_1 = "[ ]";
            }
            if (linevalin == 2)
            {
                line_highlight_2 = "[O]";
            }
            else
            {
                line_highlight_2 = "[ ]";
            }
            if (linevalin == 3)
            {
                line_highlight_3 = "[O]";
            }
            else
            {
                line_highlight_3 = "[ ]";
            }
            if (linevalin == 4)
            {
                line_highlight_4 = "[O]";
            }
            else
            {
                line_highlight_4 = "[ ]";
            }
            if (linevalin == 5)
            {
                line_highlight_5 = "[O]";
            }
            else
            {
                line_highlight_5 = "[ ]";
            }
            if (linevalin == 6)
            {
                line_highlight_6 = "[O]";
            }
            else
            {
                line_highlight_6 = "[ ]";
            }
            if (linevalin == 7)
            {
                line_highlight_7 = "[O]";
            }
            else
            {
                line_highlight_7 = "[ ]";
            }
            if (linevalin == 8)
            {
                line_highlight_8 = "[O]";
            }
            else
            {
                line_highlight_8 = "[ ]";
            }
            if (linevalin == 9)
            {
                line_highlight_9 = "[O]";
            }
            else
            {
                line_highlight_9 = "[ ]";
            }
            if (linevalin == 10)
            {
                line_highlight_10 = "[O]";
            }
            else
            {
                line_highlight_10 = "[ ]";
            }
            if (linevalin == 11)
            {
                line_highlight_11 = "[O]";
            }
            else
            {
                line_highlight_11 = "[ ]";
            }
            if (linevalin == 12)
            {
                line_highlight_12 = "[O]";
            }
            else
            {
                line_highlight_12 = "[ ]";
            }

        }

        public void screen_display()
        {
            display_view.Clear();
            if (menu_level == 0)
            {
                display_view.Append($"GMDI - {ver}");
                display_view.Append('\n');
                display_view.Append("------------");
                display_view.Append('\n');
                display_view.Append($"Main Menu - Iteration: {iteration_view} Item: {(item_number + 1)}");
                display_view.Append('\n');
                display_view.Append('\n');
            }
            if (menu_level == 1)
            {
                display_view.Append($"GMDI - {ver}");
                display_view.Append('\n');
                display_view.Append("------------");
                display_view.Append('\n');
                display_view.Append($"Command Menu - Iteration: {iteration_view} Item: {(item_number + 1)}");
                display_view.Append('\n');
                display_view.Append('\n');
            }
            if (menu_level == 2)
            {
                display_view.Append($"GMDI - {ver}");
                display_view.Append('\n');
                display_view.Append("------------");
                display_view.Append('\n');
                display_view.Append($"Mining Job Config. - Iteration: {iteration_view} Item: {(item_number + 1)}");
                display_view.Append('\n');
                display_view.Append('\n');
            }
            display_view.Append('\n');
            if (!data_valid)
            {
                display_view.Append('\n');
                display_view.Append("No target coordinates found!");
                display_view.Append('\n');
                display_view.Append("Please assign valid target using prospector.");
                display_view.Append('\n');
                display_view.Append('\n');
            }
            if (menu_level == 0)
            {
                display_view.Append($"{line_highlight_0} 1. {item_line_0[menu_level]}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_1} 2. {item_line_1[menu_level]}");
                display_view.Append('\n');
                display_view.Append('\n');
                display_view.Append('\n');
                display_view.Append($"Command: {last_command}");
                display_view.Append('\n');
            }
            if (menu_level == 1)
            {
                display_view.Append($"{line_highlight_0} 1. {scroll_command_item[scroll_item_val]}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_1} ..  {item_line_1[menu_level]}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_7} 8. {item_line_7[menu_level]} {cancel_display}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_8} 9.  {item_line_8[menu_level]} {menu_display}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_9} ..  {item_line_9[menu_level]}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_12} 12. {item_line_12[menu_level]} {displayconfirm_1}");
                if (confirm_command)
                {
                    display_view.Append('\n');
                    display_view.Append('\n');
                    display_view.Append("Command confirmed!");
                    display_view.Append('\n');
                }
                display_view.Append('\n');
                display_view.Append('\n');
                display_view.Append($"Command: {last_command}");
                display_view.Append('\n');
            }
            if (menu_level == 2)
            {
                display_view.Append($"{line_highlight_0} 1. {item_line_0[menu_level]} {new_numPointsX}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_1} 2. {item_line_1[menu_level]} {new_numPointsY}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_2} 3. {item_line_2[menu_level]} {new_gridsize}m");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_3} 4. {item_line_3[menu_level]} {new_skipbores}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_4} 5. {item_line_4[menu_level]} {new_drillshaft_length}m");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_5} 6. {item_line_5[menu_level]} {new_ignore_depth}m");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_6} 7. {item_line_6[menu_level]} {limit_display}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_7} 8. {item_line_7[menu_level]} {new_hard_drone_limit}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_8} 9. {item_line_8[menu_level]} {new_flight_factor}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_9} 10. {item_line_9[menu_level]} {core_display}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_10} 11. {item_line_10[menu_level]} {align_display}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_11} 12. {item_line_11[menu_level]} {menu_display}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_12} 13. {item_line_12[menu_level]} {displayconfirm_2}");
                if (confirm_send)
                {
                    display_view.Append('\n');
                    display_view.Append('\n');
                    display_view.Append("Data confirmed!");
                    display_view.Append('\n');
                }
            }
            if (data_valid)
            {
                display_view.Append('\n');
                display_view.Append('\n');
                display_view.Append($"Mining Job Information");
                display_view.Append('\n');
                display_view.Append($"-----------");
                display_view.Append('\n');
                display_view.Append($"Surface Distance: {surfaceDistance}");                
                display_view.Append('\n');
                display_view.Append("Target Coordinates:");
                display_view.Append('\n');
                display_view.Append($"X: {Math.Round(main_gps_coords.X, 2)}, Y: {Math.Round(main_gps_coords.Y, 2)}, Z: {Math.Round(main_gps_coords.Z, 2)}");
                display_view.Append('\n');                               
                if (customDataAlignTargetValid)
                {
                    display_view.Append("Align Coordinates:");
                    display_view.Append('\n');
                    display_view.Append($"X: {alignGPSCoordinates.X}, Y: {alignGPSCoordinates.Y}, Z: {alignGPSCoordinates.Z}");
                    display_view.Append('\n');                    
                }

            }
        }

        

        public void incr_item()
        {
            if (menu_level == 0)
            {
                item_max_limit = 1;
                item_min_limit = 0;
            }
            if (menu_level == 1)
            {
                item_max_limit = 1;
                item_min_limit = 0;
            }
            if (menu_level == 2)
            {
                item_max_limit = 12;
                item_min_limit = 0;
            }
            item_number++;
            if (item_number > item_max_limit)
            {
                item_number = item_min_limit;
            }
        }

        public void decr_item()
        {
            if (menu_level == 0)
            {
                item_max_limit = 1;
                item_min_limit = 0;
            }
            if (menu_level == 1)
            {
                item_max_limit = 12;
                item_min_limit = 0;
            }
            if (menu_level == 2)
            {
                item_max_limit = 12;
                item_min_limit = 0;

            }
            item_number--;
            if (item_number < item_min_limit)
            {
                item_number = item_max_limit;
            }
        }
        public void incr_scoll_command()
        {
            if (menu_level == 1)
            {
                scroll_item_val_min_limit = 0;
                scroll_item_val_max_limit = 6;
            }
            scroll_item_val++;
            if (scroll_item_val > scroll_item_val_max_limit)
            {
                scroll_item_val = scroll_item_val_min_limit;
            }
        }

        public void decr_scoll_command()
        {
            if (menu_level == 1)
            {
                scroll_item_val_min_limit = 0;
                scroll_item_val_max_limit = 6;
            }
            scroll_item_val--;
            if (scroll_item_val < scroll_item_val_min_limit)
            {
                scroll_item_val = scroll_item_val_max_limit;
            }
        }

        public void command_resolver()
        {
            if (scroll_item_val == 0)
            {
                disp_command = "init";
            }
            if (scroll_item_val == 1)
            {
                disp_command = "reset";
            }
            if (scroll_item_val == 2)
            {
                disp_command = "run";
            }
            if (scroll_item_val == 3)
            {
                disp_command = "recall";
            }
            if (scroll_item_val == 4)
            {
                disp_command = "eject";
            }
            if (scroll_item_val == 5)
            {
                disp_command = "freeze";
            }
            if (scroll_item_val == 6)
            {
                disp_command = "stop";
            }
            if (scroll_item_val == 7)
            {
                disp_command = "";
            }
        }
        void runicon(int state)
        {
            if (state == 0)
            {
                icon = ".---";
            }
            if (state == 1)
            {
                icon = "-.--";
            }
            if (state == 2)
            {
                icon = "--.-";
            }
            if (state == 3)
            {
                icon = "---.";
            }
        }
        void state_shifter()
        {
            stateshift++;
            if (stateshift > 3)
            {
                stateshift = 0;
            }
            runicon(stateshift);
        }
        public void drone_custom_data_check(string custominfo, int index)
        {
            Echo("Checking for drone config information..");
            String[] temp_id = custominfo.Split(':');
            Echo($"{temp_id.Length}");

            if (temp_id.Length > 0)
            {
                if (temp_id[0] != null)
                {
                    temp_id_name = temp_id[0];
                    drone_tag = temp_id_name;
                    if (temp_id_name == "" || temp_id_name == null)
                    {
                        temp_id_name = drone_tag;
                        Echo($"Resorting to default scout tag {drone_tag}");
                    }
                }
            }
            else
            {
                temp_id_name = drone_tag;
                Echo($"Resorting to default ID#.{drone_tag}");
            }
            if (temp_id.Length > 1)
            {
                if (temp_id[1] != null)
                {
                    temp_id_name_2 = temp_id[1];
                    secondary = temp_id_name_2;
                    if (temp_id_name_2 == null)
                    {
                        temp_id_name_2 = secondary;
                        Echo($"Resorting to default scout tag {secondary}");
                    }
                }
            }
            else
            {
                temp_id_name_2 = secondary;
                Echo($"Resorting to default ID#.{drone_tag}");
            }
            if (temp_id.Length == 0)
            {
                temp_id_name = drone_tag;
                temp_id_name_2 = secondary;
                Echo($"Resorting to default config {temp_id_name} {temp_id_name_2}.");
            }
            
            
            Echo($"Drone info:{drone_tag}");
            drone_controller_tag = "[" + drone_tag + " " + comms + "]";
            display_main_tag = "[" + drone_tag + " " + intfs + " " + postfix + "]";
            ant_tg = "[" + drone_tag + " " + comms + "]";
            secondary_tag = $"[{secondary}]";
            if (secondary == "" || secondary == " " || secondary == null)
            {
                secondary_tag = "";
            }
            Me.CustomName = $"GMDI Programmable Block {secondary_tag} [{drone_tag} {intfs}]";
        }

        //end program
    }
}
