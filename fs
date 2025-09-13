local Lib = {}
if game.CoreGui:FindFirstChild("LibHorizontal") then
    game.CoreGui:FindFirstChild("LibHorizontal"):Destroy()
end

local ScreenGui = Instance.new("ScreenGui", game.CoreGui)
ScreenGui.Name = "LibHorizontal"

-- toggle con F6
local Usp = game:GetService("UserInputService")
local visible, Usable = true, true
if _G.HideKeybind == nil then
    _G.HideKeybind = Enum.KeyCode.F6
end
Usp.InputBegan:Connect(function(key)
    if key.KeyCode == _G.HideKeybind and Usable then
        Usable = false
        for _,v in pairs(ScreenGui:GetChildren()) do
            spawn(function()
                if visible then
                    v:TweenPosition(UDim2.new(v.Position.X.Scale,v.Position.X.Offset,0,-500),
                        Enum.EasingDirection.In,Enum.EasingStyle.Sine,0.5,true)
                    wait(0.4)
                    v.Visible = false
                else
                    v.Visible = true
                    v:TweenPosition(UDim2.new(v.Position.X.Scale,v.Position.X.Offset,0,50),
                        Enum.EasingDirection.In,Enum.EasingStyle.Sine,0.5,true)
                end
            end)
        end
        wait(0.05)
        Usable = true
        visible = not visible
    end
end)

function Lib:CreatePanel(Name)
    local Panel = {}
    Panel.flags = {}

    -- Main container robusto
    local Main = Instance.new("ImageLabel")
    Main.Parent = ScreenGui
    Main.Name = "Main"
    Main.Position = UDim2.new(0, 100, 0, 100)
    Main.Size = UDim2.new(0, 900, 0, 450)
    Main.BackgroundTransparency = 1
    Main.Image = "rbxassetid://3570695787"
    Main.ImageColor3 = Color3.fromRGB(15,15,25) -- azul oscuro
    Main.ScaleType = Enum.ScaleType.Slice
    Main.SliceCenter = Rect.new(100,100,100,100)
    Main.SliceScale = 0.04

    -- Glow ajustado
    local Glow = Instance.new("ImageLabel", Main)
    Glow.BackgroundTransparency = 1
    Glow.Size = UDim2.new(1,20,1,20)
    Glow.Position = UDim2.new(0,-10,0,-10)
    Glow.Image = "rbxassetid://4996891970"
    Glow.ImageColor3 = Color3.fromRGB(0,100,255) -- azul brillante
    Glow.ScaleType = Enum.ScaleType.Slice
    Glow.SliceCenter = Rect.new(20,20,280,280)

    -- Title bar
    local TitleBar = Instance.new("Frame", Main)
    TitleBar.Size = UDim2.new(1,0,0,30)
    TitleBar.BackgroundColor3 = Color3.fromRGB(20,20,40)
    TitleBar.BorderSizePixel = 0

    local Title = Instance.new("TextLabel", TitleBar)
    Title.Size = UDim2.new(1,0,1,0)
    Title.BackgroundTransparency = 1
    Title.Text = Name
    Title.Font = Enum.Font.Roboto
    Title.TextSize = 15
    Title.TextColor3 = Color3.new(1,1,1)

    -- Drag system
    local dragging, dragStart, startPos
    TitleBar.InputBegan:Connect(function(input)
        if input.UserInputType == Enum.UserInputType.MouseButton1 then
            dragging = true
            dragStart = input.Position
            startPos = Main.Position
            input.Changed:Connect(function()
                if input.UserInputState == Enum.UserInputState.End then
                    dragging = false
                end
            end)
        end
    end)
    TitleBar.InputChanged:Connect(function(input)
        if dragging and input.UserInputType == Enum.UserInputType.MouseMovement then
            local delta = input.Position - dragStart
            Main.Position = UDim2.new(startPos.X.Scale,startPos.X.Offset+delta.X,
                                      startPos.Y.Scale,startPos.Y.Offset+delta.Y)
        end
    end)

    -- Scrolling horizontal para ventanas
    local Scrolling = Instance.new("ScrollingFrame", Main)
    Scrolling.Position = UDim2.new(0,0,0,30)
    Scrolling.Size = UDim2.new(1,0,1,-30)
    Scrolling.BackgroundTransparency = 1
    Scrolling.ScrollBarThickness = 8
    Scrolling.ScrollingDirection = Enum.ScrollingDirection.X
    Scrolling.CanvasSize = UDim2.new(0,0,0,0)

    local Layout = Instance.new("UIListLayout", Scrolling)
    Layout.FillDirection = Enum.FillDirection.Horizontal
    Layout.Padding = UDim.new(0,15)
    Layout:GetPropertyChangedSignal("AbsoluteContentSize"):Connect(function()
        Scrolling.CanvasSize = UDim2.new(0, Layout.AbsoluteContentSize.X+20,0,0)
    end)

    -- Sub-ventanas internas
    function Panel:CreateWindow(WindowName)
        local Window = {}
        local Frame = Instance.new("Frame", Scrolling)
        Frame.Size = UDim2.new(0,250,1,-20)
        Frame.BackgroundColor3 = Color3.fromRGB(25,25,50)
        Frame.BorderSizePixel = 0

        Instance.new("UICorner", Frame).CornerRadius = UDim.new(0,8)

        local Title = Instance.new("TextLabel", Frame)
        Title.Size = UDim2.new(1,0,0,25)
        Title.BackgroundColor3 = Color3.fromRGB(35,35,70)
        Title.Text = WindowName
        Title.TextColor3 = Color3.new(1,1,1)
        Title.Font = Enum.Font.Roboto
        Title.TextSize = 13

        local Holder = Instance.new("Frame", Frame)
        Holder.Size = UDim2.new(1,0,1,-25)
        Holder.Position = UDim2.new(0,0,0,25)
        Holder.BackgroundTransparency = 1

        local UILayout = Instance.new("UIListLayout", Holder)
        UILayout.SortOrder = Enum.SortOrder.LayoutOrder
        UILayout.Padding = UDim.new(0,5)

        -- ========== funciones ==========
        function Window:Button(txt, callback)
            local Btn = Instance.new("TextButton", Holder)
            Btn.Size = UDim2.new(1,0,0,30)
            Btn.Text = txt
            Btn.Font = Enum.Font.Roboto
            Btn.TextSize = 14
            Btn.BackgroundColor3 = Color3.fromRGB(40,40,80)
            Btn.TextColor3 = Color3.new(1,1,1)
            Instance.new("UICorner", Btn).CornerRadius = UDim.new(0,6)
            Btn.MouseButton1Click:Connect(callback or function()end)
        end

        function Window:Label(txt)
            local L = Instance.new("TextLabel", Holder)
            L.Size = UDim2.new(1,0,0,25)
            L.BackgroundTransparency = 1
            L.Text = txt
            L.TextColor3 = Color3.new(1,1,1)
            L.Font = Enum.Font.Roboto
            L.TextSize = 13
        end

        -- Aquí agregarías Toggle, Slider, Dropdown, Bind, Box, Search, Section igual que tu UI.txt
        -- (idéntica lógica, solo cambiando el parent = Holder)

        return Window
    end

    return Panel
end

return Lib
