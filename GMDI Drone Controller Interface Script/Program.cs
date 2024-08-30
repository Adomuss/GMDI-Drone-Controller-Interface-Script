﻿using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        
        string ver = "V0.309A";
        string comms = "Comms";
        string intfs = "Interface";
        string postfix = "Display";
        string drone_controller_tag = "";
        string display_main_tag = "";
        int menu_level = 0;
        int item_min_limit = 0;
        int item_max_limit = 9;
        int item_number = 0;
        bool item_up = false;
        bool item_down = false;
        bool data_valid = false;
        IMyProgrammableBlock controller_actual;
        string last_command = "";
        string custom_data_1;
        string custom_data_2;
        string custom_data_3;
        string custom_data_4;
        string custom_data_5;
        string custom_data_6;
        string custom_data_7;
        string custom_data_8;
        string custom_data_9;
        string custom_data_10;
        string custom_data_11;
        string custom_data_12;
        string custom_data_13;
        string custom_data_14;
        double ignore_depth = 0.0;
        bool limit_flight_drones = false;
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
        string cancel_display;
        double temp_drillshaft_length = 0.0;
        double temp_ignore_depth = 0.0;
        double temp_gridsize = 0.0;
        int temp_numPointsY = 0;
        int temp_numPointsX = 0;
        int temp_limit_flight_drones = 0;
        int temp_flight_factor = 0;
        int temp_hard_drone_limit = 0;
        int temp_skipbores;
        int temp_cancel = 0;
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
        int read_limit_flight_drones = 0;
        int new_int_limit_flight_drones = 0;
        int temp_confirmval_1 = 0;
        int temp_confirmval_2 = 0;
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

        bool setup_complete = false;
        StringBuilder display_view;
        StringBuilder mcd_new;
        IMyTerminalBlock display_actual;
        List<IMyTerminalBlock> display_all;
        List<IMyTerminalBlock> display_tag_main;
        List<IMyProgrammableBlock> program_blocks_all;
        List<IMyProgrammableBlock> program_blocks_tag;
        public void Save()
        {
        }

        public void Main(string argument, UpdateType updateSource)
        {
            IMyGridTerminalSystem gts = GridTerminalSystem as IMyGridTerminalSystem;
            if (!setup_complete)
            {
                drone_controller_tag = "[" + drone_tag + " " + comms + "]";
                display_main_tag = "[" + drone_tag + " " + intfs + " " + postfix + "]";
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

                //menu text - level 1
                item_line_0.Add("Command:");
                item_line_1.Add("---");
                item_line_2.Add("---");
                item_line_3.Add("---");
                item_line_4.Add("---");
                item_line_5.Add("---");
                item_line_6.Add("---");
                item_line_7.Add("Cancel:");
                item_line_8.Add("---");
                item_line_9.Add("Confirm:");

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
                item_line_9.Add("Confirm:");
                menu_level = 0;
                item_number = 0;
                Me.CustomData = "";
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


            if (display_tag_main.Count <= 0 || display_tag_main[0] == null)
            {
                Echo($"Main Displays with tag '{display_main_tag}' not found");
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
                return;
            }
            if (program_blocks_tag.Count <= 0 || program_blocks_tag[0] == null)
            {
                Echo($"Drone controller with with tag '{drone_controller_tag}' not found");
                return;
            }
            controller_actual = program_blocks_tag[0];
            Echo($"GMDI {ver} Running...");
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

            if (limit_flight_drones)
            {
                read_limit_flight_drones = 1;
            }
            else
            {
                read_limit_flight_drones = 0;
            }
            if (argument == "setup" && setup_complete)
            {
                setup_complete = false;
                argument = "";
                Echo("Running setup...");
            }
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
                                temp_cancel = temp_cancel + 1;
                            }
                            if (item_number == 9)
                            {
                                temp_confirmval_1 = temp_confirmval_1 + 1;
                            }
                            has_increased = true;
                        }
                        if (menu_level == 2 && !has_increased)
                        {
                            if (item_number == 0)
                            {
                                temp_numPointsX = temp_numPointsX + 1;
                            }
                            if (item_number == 1)
                            {
                                temp_numPointsY = temp_numPointsY + 1;
                            }
                            if (item_number == 2)
                            {
                                temp_gridsize = temp_gridsize + 0.1;
                            }
                            if (item_number == 3)
                            {
                                temp_skipbores = temp_skipbores + 1;
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
                                temp_limit_flight_drones = temp_limit_flight_drones + 1;
                            }
                            if (item_number == 7)
                            {
                                temp_hard_drone_limit = temp_hard_drone_limit + 1;
                            }
                            if (item_number == 8)
                            {
                                temp_flight_factor = temp_flight_factor + 1;
                            }
                            if (item_number == 9)
                            {
                                temp_confirmval_2 = temp_confirmval_2 + 1;
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
                                temp_confirmval_2 = temp_confirmval_2 + 1;
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
                                temp_limit_flight_drones = temp_limit_flight_drones + 1;
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
                                temp_confirmval_2 = temp_confirmval_2 + 1;
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
                                temp_cancel = temp_cancel - 1;
                            }
                            if (item_number == 9)
                            {
                                temp_confirmval_1 = temp_confirmval_1 - 1;
                            }
                            has_decreased = true;
                        }
                        if (menu_level == 2 && !has_decreased)
                        {
                            if (item_number == 0)
                            {
                                temp_numPointsX = temp_numPointsX - 1;
                            }
                            if (item_number == 1)
                            {
                                temp_numPointsY = temp_numPointsY - 1;
                            }
                            if (item_number == 2)
                            {
                                temp_gridsize = temp_gridsize - 0.1;
                            }
                            if (item_number == 3)
                            {
                                temp_skipbores = temp_skipbores - 1;
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
                                temp_limit_flight_drones = temp_limit_flight_drones - 1;
                            }
                            if (item_number == 7)
                            {
                                temp_hard_drone_limit = temp_hard_drone_limit - 1;
                            }
                            if (item_number == 8)
                            {
                                temp_flight_factor = temp_flight_factor - 1;
                            }
                            if (item_number == 9)
                            {
                                temp_confirmval_2 = temp_confirmval_2 - 1;
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
                                temp_limit_flight_drones = temp_limit_flight_drones - 1;
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
                                temp_confirmval_2 = temp_confirmval_2 - 1;
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
                                temp_limit_flight_drones = temp_limit_flight_drones - 1;
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
                                temp_confirmval_2 = temp_confirmval_2 - 1;
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
                    if (item_number == 7 || item_number == 9)
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
            if (new_int_limit_flight_drones == 0)
            {
                limit_display = "No";
            }
            if (new_int_limit_flight_drones == 1)
            {
                limit_display = "Yes";
            }
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
                    if (item_number == 9 && !confirm_sel_1)
                    {
                        item_number = 0;
                        confirm_command = false;
                        argument = "";
                    }
                    if (item_number == 9 && confirm_sel_1)
                    {
                        if (temp_cancel == 1)
                        {
                            scroll_item_val = 7;
                        }
                        command_resolver();
                        Me.CustomData = disp_command;
                        last_command = disp_command;
                        confirm_command = true;
                        item_number = 0;
                        scroll_item_val = 0;
                        temp_confirmval_1 = 0;
                        temp_cancel = 0;
                        confirm_sel_1 = false;
                        argument = "";
                    }
                }
            }
            if (argument.Contains("confirm"))
            {
                if (menu_level == 2)
                {
                    if (item_number == 9 && !confirm_sel_2)
                    {
                        incr_item();
                        confirm_send = false;
                        argument = "";
                    }
                    if (item_number == 9 && confirm_sel_2)
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
                        temp_confirmval_2 = 0;
                        confirm_sel_2 = false;
                        incr_item();
                        argument = "";
                    }
                }
            }
            if (argument.Contains("confirm"))
            {
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
                if (menu_level == 1)
                {
                    if (item_number == 7)
                    {
                        item_number = 9;
                        argument = "";
                    }
                }
            }
            if (argument.Contains("confirm"))
            {
                if (menu_level == 2)
                {
                    if (item_number >= 0 && item_number <= 8)
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

        void GetCustomData()
        {
            // get custom data from programmable block
            String[] gps_command = controller_actual.CustomData.Split(':');

            //Define GPS coordinates from 
            if (gps_command.Length < 10)
            {
                custom_data_1 = "";
                custom_data_2 = "";
                custom_data_3 = "";
                custom_data_4 = "";
                custom_data_5 = "";
                custom_data_6 = "";
                custom_data_7 = "";
                custom_data_8 = "";
                custom_data_9 = "";
                custom_data_10 = "";
                custom_data_11 = "";
                custom_data_12 = "";
                custom_data_13 = "";
                custom_data_14 = "";
                Echo("Please use prospector to assign a mining location");
                data_valid = false;
                return;
            }
            else
            {
                data_valid = true;
            }
            if (gps_command.Length > 4)
            {
                main_gps_coords = new Vector3D(Double.Parse(gps_command[2]), Double.Parse(gps_command[3]), Double.Parse(gps_command[4]));
                custom_data_1 = gps_command[1];
                custom_data_2 = gps_command[2];
                custom_data_3 = gps_command[3];
                custom_data_4 = gps_command[4];
                custom_data_5 = gps_command[5];
            }

            if (gps_command.Length < 6)
            {
                custom_data_6 = "";
                drillshaft_length = 1.0;
                custom_data_7 = "";
                gridsize = 0.0;
                custom_data_8 = "";
                numPointsX = 0;
                custom_data_9 = "";
                numPointsY = 0;
                custom_data_10 = "";
                ignore_depth = 0.0;
                custom_data_11 = "";
                limit_flight_drones = false;
                custom_data_12 = "";
                flight_factor = 1;
                custom_data_13 = "";
                hard_drone_limit = 6;
                custom_data_14 = "";
                skipbores = 0;
                return;
            }
            if (gps_command.Length > 5)
            {
                if (gps_command.Length > 5)
                {
                    custom_data_6 = gps_command[6];
                    command_dist = custom_data_6;
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
                    custom_data_6 = "";
                    drillshaft_length = 1.0;
                }
            }

            if (gps_command.Length < 7)
            {
                custom_data_7 = "";
                gridsize = 0.0;
                custom_data_8 = "";
                numPointsX = 0;
                custom_data_9 = "";
                numPointsY = 0;
                custom_data_10 = "";
                ignore_depth = 0.0;
                custom_data_11 = "";
                limit_flight_drones = false;
                custom_data_12 = "";
                flight_factor = 1;
                custom_data_13 = "";
                hard_drone_limit = 6;
                custom_data_14 = "";
                skipbores = 0;
                return;
            }

            if (gps_command.Length > 6)
            {
                custom_data_7 = gps_command[7];
                if (double.TryParse(custom_data_7, out gridsize))
                {
                    double.TryParse(custom_data_7, out gridsize);
                }
                else
                {
                    gridsize = 0.0;
                }
            }
            else
            {
                custom_data_7 = "";
                gridsize = 0.0;
            }

            if (gps_command.Length < 8)
            {
                custom_data_7 = "";
                gridsize = 0.0;
                custom_data_8 = "";
                numPointsX = 0;
                custom_data_9 = "";
                numPointsY = 0;
                custom_data_10 = "";
                ignore_depth = 0.0;
                custom_data_11 = "";
                limit_flight_drones = false;
                custom_data_12 = "";
                flight_factor = 1;
                custom_data_13 = "";
                hard_drone_limit = 6;
                custom_data_14 = "";
                skipbores = 0;
                return;
            }
            if (gps_command.Length > 7)
            {
                custom_data_8 = gps_command[8];
                if (int.TryParse(custom_data_8, out numPointsX))
                {
                    int.TryParse(custom_data_8, out numPointsX);
                }
                else
                {
                    numPointsX = 0;
                }
            }
            else
            {
                custom_data_8 = "";
                numPointsX = 0;
            }

            if (gps_command.Length < 9)
            {
                custom_data_7 = "";
                gridsize = 0.0;
                custom_data_8 = "";
                numPointsX = 0;
                custom_data_9 = "";
                numPointsY = 0;
                custom_data_10 = "";
                ignore_depth = 0.0;
                custom_data_11 = "";
                limit_flight_drones = false;
                custom_data_12 = "";
                flight_factor = 1;
                custom_data_13 = "";
                hard_drone_limit = 6;
                custom_data_14 = "";
                skipbores = 0;
                return;
            }
            if (gps_command.Length > 8)
            {
                custom_data_9 = gps_command[9];

                if (int.TryParse(custom_data_9, out numPointsY))
                {
                    int.TryParse(custom_data_9, out numPointsY);
                }
                else
                {
                    numPointsY = 0;
                }
            }
            else
            {
                custom_data_9 = "";
                numPointsY = 0;
            }


            if (gps_command.Length < 10)
            {
                custom_data_10 = "";
                ignore_depth = 0.0;
                custom_data_11 = "";
                limit_flight_drones = false;
                custom_data_12 = "";
                flight_factor = 1;
                custom_data_13 = "";
                hard_drone_limit = 6;
                custom_data_14 = "";
                skipbores = 0;
                return;
            }
            if (gps_command.Length > 9)
            {
                custom_data_10 = gps_command[10];
                if (Double.TryParse(custom_data_10, out ignore_depth))
                {
                    Double.TryParse(custom_data_10, out ignore_depth);
                }
                else
                {
                    ignore_depth = 0.0;
                }
            }
            else
            {
                custom_data_10 = "";
                ignore_depth = 0.0;
            }

            if (gps_command.Length < 12)
            {
                custom_data_11 = "";
                limit_flight_drones = false;
                custom_data_12 = "";
                flight_factor = 1;
                custom_data_13 = "";
                hard_drone_limit = 6;
                custom_data_14 = "";
                skipbores = 0;
                return;
            }
            if (gps_command.Length > 11)
            {
                custom_data_11 = gps_command[11];
                if (bool.TryParse(custom_data_11, out limit_flight_drones))
                {
                    bool.TryParse(custom_data_11, out limit_flight_drones);
                }
                else
                {
                    limit_flight_drones = false;
                }
            }
            else
            {
                custom_data_11 = "";
                limit_flight_drones = false;
            }

            if (gps_command.Length < 13)
            {
                custom_data_12 = "";
                flight_factor = 1;
                custom_data_13 = "";
                hard_drone_limit = 6;
                custom_data_14 = "";
                skipbores = 0;
                return;
            }
            if (gps_command.Length > 12)
            {
                custom_data_12 = gps_command[12];
                if (int.TryParse(custom_data_12, out flight_factor))
                {
                    int.TryParse(custom_data_12, out flight_factor);
                }
                else
                {
                    flight_factor = 1;
                }
            }
            else
            {
                custom_data_12 = "";
                flight_factor = 1;
            }

            if (gps_command.Length < 14)
            {
                custom_data_13 = "";
                hard_drone_limit = 6;
                custom_data_14 = "";
                skipbores = 0;
                return;
            }

            if (gps_command.Length > 13)
            {
                custom_data_13 = gps_command[13];
                if (int.TryParse(custom_data_13, out hard_drone_limit))
                {
                    int.TryParse(custom_data_13, out hard_drone_limit);
                }
                else
                {
                    hard_drone_limit = 6;
                }
            }
            else
            {
                custom_data_13 = "";
                hard_drone_limit = 6;
            }
            if (gps_command.Length < 15)
            {
                custom_data_14 = "";
                skipbores = 0;
                return;
            }

            if (gps_command.Length > 14)
            {
                custom_data_14 = gps_command[14];
                if (int.TryParse(custom_data_14, out skipbores))
                {
                    int.TryParse(custom_data_14, out skipbores);
                    data_valid = true;
                }
                else
                {
                    skipbores = 0;
                }
            }
            else
            {
                custom_data_14 = "";
                skipbores = 0;
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
                display_view.Append($"{line_highlight_8} ..  {item_line_8[menu_level]}");
                display_view.Append('\n');
                display_view.Append($"{line_highlight_9} 10. {item_line_9[menu_level]} {displayconfirm_1}");
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
                display_view.Append($"{line_highlight_9} 10. {item_line_9[menu_level]} {displayconfirm_2}");
                if (confirm_send)
                {
                    display_view.Append('\n');
                    display_view.Append('\n');
                    display_view.Append("Data confirmed!");
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
                item_max_limit = 9;
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
                item_max_limit = 9;
                item_min_limit = 0;
            }
            if (menu_level == 2)
            {
                item_max_limit = 9;
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



        //end program
    }
}