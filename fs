local Lib = {}
if game.CoreGui:FindFirstChild("LibHorizontal") then
    game.CoreGui:FindFirstChild("LibHorizontal"):Destroy()
end

local ScreenGui = Instance.new("ScreenGui", game.CoreGui)
ScreenGui.Name = "LibHorizontal"

-- toggle con F6
local Usp = game:GetService("UserInputService")
local visible = true
local Usable = true
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
    Main.Size = UDim2.new(0, 800, 0, 400)
    Main.BackgroundTransparency = 1
    Main.Image = "rbxassetid://3570695787"
    Main.ImageColor3 = Color3.fromRGB(10,10,10)
    Main.ScaleType = Enum.ScaleType.Slice
    Main.SliceCenter = Rect.new(100,100,100,100)
    Main.SliceScale = 0.04

    -- Glow
    local Glow = Instance.new("ImageLabel", Main)
    Glow.BackgroundTransparency = 1
    Glow.Size = UDim2.new(1,40,1,40)
    Glow.Position = UDim2.new(0,-20,0,-20)
    Glow.Image = "rbxassetid://4996891970"
    Glow.ImageColor3 = Color3.fromRGB(255,0,0)
    Glow.ScaleType = Enum.ScaleType.Slice
    Glow.SliceCenter = Rect.new(20,20,280,280)

    -- Title bar
    local TitleBar = Instance.new("Frame", Main)
    TitleBar.Size = UDim2.new(1,0,0,30)
    TitleBar.BackgroundColor3 = Color3.fromRGB(20,20,20)
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

    -- Zona de “ventanas internas” (scroll horizontal si no caben)
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

    -- Crear subventanas
    function Panel:CreateWindow(WindowName)
        local Window = Instance.new("Frame", Scrolling)
        Window.Size = UDim2.new(0,200,1,-20)
        Window.BackgroundColor3 = Color3.fromRGB(30,30,30)
        Window.BorderSizePixel = 0

        local Corner = Instance.new("UICorner", Window)
        Corner.CornerRadius = UDim.new(0,6)

        local Title = Instance.new("TextLabel", Window)
        Title.Size = UDim2.new(1,0,0,25)
        Title.BackgroundColor3 = Color3.fromRGB(45,45,45)
        Title.Text = WindowName
        Title.TextColor3 = Color3.new(1,1,1)
        Title.Font = Enum.Font.Roboto
        Title.TextSize = 13

        local Holder = Instance.new("Frame", Window)
        Holder.Size = UDim2.new(1,0,1,-25)
        Holder.Position = UDim2.new(0,0,0,25)
        Holder.BackgroundTransparency = 1

        local Layout = Instance.new("UIListLayout", Holder)
        Layout.SortOrder = Enum.SortOrder.LayoutOrder
        Layout.Padding = UDim.new(0,5)

        -- Aquí puedes replicar las funciones Button, Toggle, Slider, etc. como en tu UI.txt
        -- pero ahora metiéndolos dentro de "Holder" de cada ventana
        -- location[flag] se sigue manejando igual

        return Holder
    end

    return Panel
end

return Lib
