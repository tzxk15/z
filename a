-- Chaos UI - Botones en columna vertical (fila india hacia abajo)
-- Tema: Negro mate (3,3,3) + glow azul rey
-- Incluye: Buttons (columna vertical con glow), Labels, Toggles, Boxes, Sliders, Dropdowns, Binds
-- Draggable, Resizable (PC + Mobile), Hide con F6

local Lib = {}
if game.CoreGui:FindFirstChild("LibVerticalChaos") then
    game.CoreGui.LibVerticalChaos:Destroy()
end

local ScreenGui = Instance.new("ScreenGui", game.CoreGui)
ScreenGui.Name = "LibVerticalChaos"
ScreenGui.ZIndexBehavior = Enum.ZIndexBehavior.Global
ScreenGui.IgnoreGuiInset = true
ScreenGui.ResetOnSpawn = false

local UserInputService = game:GetService("UserInputService")
local visible, usable = true, true
if _G.HideKeybind == nil then _G.HideKeybind = Enum.KeyCode.F6 end

UserInputService.InputBegan:Connect(function(inp, gp)
    if gp then return end
    if inp.KeyCode == _G.HideKeybind and usable then
        usable = false
        for _, v in pairs(ScreenGui:GetChildren()) do
            if v:IsA("Frame") or v:IsA("ImageLabel") then
                spawn(function()
                    if visible then
                        v:TweenPosition(UDim2.new(v.Position.X.Scale, v.Position.X.Offset, 0, -600),
                            Enum.EasingDirection.In, Enum.EasingStyle.Sine, 0.45, true)
                        wait(0.35)
                        v.Visible = false
                    else
                        v.Visible = true
                        v:TweenPosition(UDim2.new(v.Position.X.Scale, v.Position.X.Offset, 0, 120),
                            Enum.EasingDirection.Out, Enum.EasingStyle.Sine, 0.45, true)
                    end
                end)
            end
        end
        wait(0.07)
        usable = true
        visible = not visible
    end
end)

function Lib:CreatePanel(name)
    local Panel = {}
    Panel.flags = {}
    Panel.windows = {}

    -- Main frame
    local Main = Instance.new("Frame", ScreenGui)
    Main.Name = "Main"
    Main.Position = UDim2.new(0,150,0,150)
    Main.Size = UDim2.new(0,900,0,520)
    Main.BackgroundColor3 = Color3.fromRGB(3,3,3)
    Main.BackgroundTransparency = 0.12
    Main.BorderSizePixel = 0
    Main.ZIndex = 2
    local mc = Instance.new("UICorner", Main); mc.CornerRadius = UDim.new(0,12)

    -- Glow (pegado al borde)
    local Glow = Instance.new("ImageLabel", Main)
    Glow.Name = "Glow"
    Glow.BackgroundTransparency = 1
    Glow.AnchorPoint = Vector2.new(0.5,0.5)
    Glow.Position = UDim2.new(0.5,0,0.5,0)
    Glow.Size = UDim2.new(1,18,1,18)
    Glow.ZIndex = 1
    Glow.Image = "rbxassetid://5028857084"
    Glow.ImageColor3 = Color3.fromRGB(0,80,200)
    Glow.ImageTransparency = 0.28
    Glow.ScaleType = Enum.ScaleType.Slice
    Glow.SliceCenter = Rect.new(24,24,276,276)

    -- TitleBar
    local TitleBar = Instance.new("Frame", Main)
    TitleBar.Name = "TitleBar"
    TitleBar.Size = UDim2.new(1,0,0,44)
    TitleBar.Position = UDim2.new(0,0,0,0)
    TitleBar.BackgroundColor3 = Color3.fromRGB(12,12,12)
    TitleBar.BackgroundTransparency = 0.06
    TitleBar.BorderSizePixel = 0
    TitleBar.ZIndex = 3
    local tb = Instance.new("UICorner", TitleBar); tb.CornerRadius = UDim.new(0,10)

    local Title = Instance.new("TextLabel", TitleBar)
    Title.Size = UDim2.new(1,0,1,0)
    Title.BackgroundTransparency = 1
    Title.Text = name or "Chaos Adaptado"
    Title.Font = Enum.Font.GothamBold
    Title.TextSize = 16
    Title.TextColor3 = Color3.fromRGB(225,225,255)
    Title.ZIndex = 4

    -- Drag (mouse + touch)
    local dragging, dragStart, startPos = false, nil, nil
    TitleBar.InputBegan:Connect(function(input)
        if input.UserInputType == Enum.UserInputType.MouseButton1 or input.UserInputType == Enum.UserInputType.Touch then
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
    UserInputService.InputChanged:Connect(function(input)
        if dragging and (input.UserInputType == Enum.UserInputType.MouseMovement or input.UserInputType == Enum.UserInputType.Touch) then
            local delta = input.Position - dragStart
            Main.Position = UDim2.new(startPos.X.Scale, startPos.X.Offset + delta.X,
                                      startPos.Y.Scale, startPos.Y.Offset + delta.Y)
        end
    end)

    -- Resizer (gris oscuro)
    local Resizer = Instance.new("Frame", Main)
    Resizer.Name = "Resizer"
    Resizer.Size = UDim2.new(0,24,0,24)
    Resizer.AnchorPoint = Vector2.new(1,1)
    Resizer.Position = UDim2.new(1,4,1,4)
    Resizer.BackgroundColor3 = Color3.fromRGB(40,40,40)
    Resizer.BackgroundTransparency = 0.15
    Resizer.BorderSizePixel = 0
    Resizer.ZIndex = 5
    local rcr = Instance.new("UICorner", Resizer); rcr.CornerRadius = UDim.new(1,0)

    local resizing, resizeStart, startSize = false, nil, nil
    Resizer.InputBegan:Connect(function(input)
        if input.UserInputType == Enum.UserInputType.MouseButton1 or input.UserInputType == Enum.UserInputType.Touch then
            resizing = true
            resizeStart = input.Position
            startSize = { X = Main.AbsoluteSize.X, Y = Main.AbsoluteSize.Y }
            input.Changed:Connect(function()
                if input.UserInputState == Enum.UserInputState.End then
                    resizing = false
                end
            end)
        end
    end)
    UserInputService.InputChanged:Connect(function(input)
        if resizing and (input.UserInputType == Enum.UserInputType.MouseMovement or input.UserInputType == Enum.UserInputType.Touch) then
            local delta = input.Position - resizeStart
            local newX = math.max(400, math.floor(startSize.X + delta.X))
            local newY = math.max(260, math.floor(startSize.Y + delta.Y))
            Main.Size = UDim2.new(0, newX, 0, newY)
        end
    end)

    -- Tabs Row
    local Tabs = Instance.new("Frame", Main)
    Tabs.Name = "Tabs"
    Tabs.Position = UDim2.new(0,0,0,44)
    Tabs.Size = UDim2.new(1,0,0,40)
    Tabs.BackgroundTransparency = 1
    Tabs.ZIndex = 3
    local TabsLayout = Instance.new("UIListLayout", Tabs)
    TabsLayout.FillDirection = Enum.FillDirection.Horizontal
    TabsLayout.Padding = UDim.new(0,8)
    TabsLayout.HorizontalAlignment = Enum.HorizontalAlignment.Left

    -- Content area
    local Content = Instance.new("Frame", Main)
    Content.Name = "Content"
    Content.Position = UDim2.new(0,0,0,88)
    Content.Size = UDim2.new(1,0,1,-92)
    Content.BackgroundColor3 = Color3.fromRGB(5,5,5)
    Content.BackgroundTransparency = 0.08
    Content.BorderSizePixel = 0
    Content.ZIndex = 3
    local contentCorner = Instance.new("UICorner", Content); contentCorner.CornerRadius = UDim.new(0,10)

    -- Function to create a window/tab
    function Panel:CreateWindow(windowName)
        local selfwin = {}
        local TabBtn = Instance.new("TextButton", Tabs)
        TabBtn.Size = UDim2.new(0,140,1,0)
        TabBtn.Text = windowName
        TabBtn.Font = Enum.Font.Gotham
        TabBtn.TextSize = 14
        TabBtn.BackgroundColor3 = Color3.fromRGB(18,18,18)
        TabBtn.BackgroundTransparency = 0.08
        TabBtn.TextColor3 = Color3.fromRGB(220,220,255)
        TabBtn.AutoButtonColor = true
        local tabCorner = Instance.new("UICorner", TabBtn); tabCorner.CornerRadius = UDim.new(0,6)

        local Holder = Instance.new("ScrollingFrame", Content)
        Holder.Size = UDim2.new(1,0,1,0)
        Holder.CanvasSize = UDim2.new(0,0,0,0)
        Holder.BackgroundTransparency = 1
        Holder.ScrollBarThickness = 8
        Holder.Visible = false
        Holder.ZIndex = 4

-- Dentro del ButtonRow
local GridLayout = Instance.new("UIGridLayout", ButtonRow)
GridLayout.CellSize = UDim2.new(0,150,0,40) -- cada botón mide 150x40
GridLayout.CellPadding = UDim2.new(0,12,0,12) -- separación entre botones
GridLayout.FillDirection = Enum.FillDirection.Horizontal
GridLayout.SortOrder = Enum.SortOrder.LayoutOrder
GridLayout.StartCorner = Enum.StartCorner.TopLeft

        -- ButtonRow (vertical)
        local ButtonRow = Instance.new("Frame", Holder)
        ButtonRow.Name = "ButtonRow"
        ButtonRow.Size = UDim2.new(1,-20,0,0)
        ButtonRow.Position = UDim2.new(0,10,0,0)
        ButtonRow.BackgroundTransparency = 1
        ButtonRow.ZIndex = 4

        local ButtonLayout = Instance.new("UIListLayout", ButtonRow)
        ButtonLayout.FillDirection = Enum.FillDirection.Vertical
        ButtonLayout.HorizontalAlignment = Enum.HorizontalAlignment.Left
        ButtonLayout.SortOrder = Enum.SortOrder.LayoutOrder
        ButtonLayout.Padding = UDim.new(0,10)

        -- OtherRow for extra controls
        local OtherRow = Instance.new("Frame", Holder)
        OtherRow.Name = "OtherRow"
        OtherRow.Size = UDim2.new(1,-20,0,0)
        OtherRow.Position = UDim2.new(0,10,0,0)
        OtherRow.BackgroundTransparency = 1
        OtherRow.ZIndex = 4
        local OtherLayout = Instance.new("UIListLayout", OtherRow)
        OtherLayout.FillDirection = Enum.FillDirection.Vertical
        OtherLayout.SortOrder = Enum.SortOrder.LayoutOrder
        OtherLayout.Padding = UDim.new(0,8)

        -- Show/hide holder
        TabBtn.MouseButton1Click:Connect(function()
            for _, c in pairs(Content:GetChildren()) do
                if c:IsA("ScrollingFrame") then
                    c.Visible = false
                end
            end
            Holder.Visible = true
        end)

        -- Create a Button
        function selfwin:Button(text, callback)
            local BtnContainer = Instance.new("Frame", ButtonRow)
            BtnContainer.Size = UDim2.new(1,0,0,40)
            BtnContainer.BackgroundTransparency = 1
            BtnContainer.LayoutOrder = 1

            local glow = Instance.new("ImageLabel", BtnContainer)
            glow.Name = "BtnGlow"
            glow.AnchorPoint = Vector2.new(0.5,0.5)
            glow.Position = UDim2.new(0.5,0,0.5,0)
            glow.Size = UDim2.new(1.02,0,1.4,0)
            glow.BackgroundTransparency = 1
            glow.Image = "rbxassetid://5028857084"
            glow.ImageColor3 = Color3.fromRGB(0,80,200)
            glow.ImageTransparency = 0.26
            glow.ScaleType = Enum.ScaleType.Slice
            glow.SliceCenter = Rect.new(24,24,276,276)
            glow.ZIndex = 3

            local Btn = Instance.new("TextButton", BtnContainer)
            Btn.Size = UDim2.new(1,0,1,0)
            Btn.Text = text
            Btn.Font = Enum.Font.GothamBold
            Btn.TextSize = 14
            Btn.BackgroundColor3 = Color3.fromRGB(20,20,20)
            Btn.BackgroundTransparency = 0.08
            Btn.TextColor3 = Color3.fromRGB(235,235,255)
            Btn.ZIndex = 4
            local bcr = Instance.new("UICorner", Btn); bcr.CornerRadius = UDim.new(0,8)

            Btn.MouseButton1Click:Connect(function()
                pcall(function() if callback then callback() end end)
            end)

            return Btn
        end

        return selfwin
    end

    return Panel
end

return Lib
