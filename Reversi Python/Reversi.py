import pygame
import copy


def loadImages(path, size):
    img = pygame.image.load(f'{path}').convert_alpha()
    img = pygame.transform.scale(img, size)
    return img


def loadSpriteSheet(sheet, row, col, newSize, size):
    # creates an empty surface
    image = pygame.Surface((32, 32)).convert_alpha()
    image.blit(sheet, (0, 0), (row * size[0], col * size[0], size[0], size[1]))
    image = pygame.transform.scale(image, newSize)
    image.set_colorkey('Black')
    return image


def directions(x, y, minX=0, minY=0, maxX=7, maxY=7):
    # Check which directions are valid
    validDirections = []
    if x != minX:
        validDirections.append((x - 1, y))
    if x != minX and y != minY:
        validDirections.append((x - 1, y - 1))
    if x != minX and y != maxY:
        validDirections.append((x - 1, y + 1))

    if x != maxX:
        validDirections.append((x + 1, y))
    if x != maxX and y != minY:
        validDirections.append((x + 1, y - 1))
    if x != maxX and y != maxY:
        validDirections.append((x + 1, y + 1))

    if y != minY:
        validDirections.append((x, y - 1))
    if y != maxY:
        validDirections.append((x, y + 1))

    return validDirections


def evaluateBoard(grid, currentPlayer):
    score = 0
    for y, row in enumerate(grid):
        for x, column in enumerate(row):
            score -= column
    return score


class Reversi:
    # display
    def __init__(self):
        pygame.init()
        self.screen = pygame.display.set_mode((1100, 800))
        pygame.display.set_caption('Reversi')

        self.player1 = 1
        self.player2 = -1

        self.currentPlayer = 1

        self.rows = 8
        self.columns = 8

        self.grid = Grid(self.rows, self.columns, (80, 80), self)

        self.computer = Computer(self.grid)

        self.time = 0

        self.gameOver = True

        self.RUN = True

    def run(self):
        while self.RUN:
            self.input()
            self.update()
            self.draw()

    def input(self):
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                self.RUN = False

            if event.type == pygame.MOUSEBUTTONDOWN:
                if event.button == 3:
                    self.grid.printGameLogicBoard()

                if event.button == 1:
                    if self.currentPlayer == 1 and not self.gameOver:
                        x, y = pygame.mouse.get_pos()
                        x, y = (x - 80) // 80, (y - 80) // 80
                        validCells = self.grid.findAvailabeCells(self.grid.gridLogic, self.currentPlayer)
                        if not validCells:
                            pass
                        else:
                            if (y, x) in validCells:
                                self.grid.insertToken(self.grid.gridLogic, self.currentPlayer, y, x)
                                swappableTiles = self.grid.swappableTiles(y, x, self.grid.gridLogic, self.currentPlayer)
                                for tile in swappableTiles:
                                    self.grid.animateTransition(tile, self.currentPlayer)
                                    self.grid.gridLogic[tile[0]][tile[1]] *= -1
                                self.currentPlayer *= -1
                                self.time = pygame.time.get_ticks()

                    if self.gameOver:
                        x, y = pygame.mouse.get_pos()
                        if 320 <= x <= 480 and 400 <= y <= 480:
                            self.grid.newGame()
                            self.gameOver = False

    def update(self):
        if self.currentPlayer == -1:
            newTime = pygame.time.get_ticks()
            if newTime - self.time >= 1000:
                if not self.grid.findAvailabeCells(self.grid.gridLogic, self.currentPlayer):
                    self.gameOver = True
                    return
                cell, score = self.computer.minimax(self.grid.gridLogic, 5, -64, 64, -1)
                self.grid.insertToken(self.grid.gridLogic, self.currentPlayer, cell[0], cell[1])
                swappableTiles = self.grid.swappableTiles(cell[0], cell[1], self.grid.gridLogic, self.currentPlayer)
                for tile in swappableTiles:
                    self.grid.animateTransition(tile, self.currentPlayer)
                    self.grid.gridLogic[tile[0]][tile[1]] *= -1
                self.currentPlayer *= -1

        self.grid.player1Score = self.grid.calculatePlayerScore(self.player1)
        self.grid.player2Score = self.grid.calculatePlayerScore(self.player2)
        if not self.grid.findAvailabeCells(self.grid.gridLogic, self.currentPlayer):
            self.gameOver = True
            return

    def draw(self):
        self.screen.fill((0, 0, 0))
        self.grid.drawGrid(self.screen)
        pygame.display.update()


class Grid:
    def __init__(self, rows, columns, size, main):
        self.GAME = main
        self.y = rows
        self.x = columns
        self.size = size

        self.whitetoken = loadImages('assets/WhiteToken.png', size)
        self.blacktoken = loadImages('assets/BlackToken.png', size)
        self.transitionBlackToWhite = [loadImages(f'assets/BlackToWhite{i}.png', self.size) for i in range(1, 4)]
        self.transitionWhiteToBlack = [loadImages(f'assets/WhiteToBlack{i}.png', self.size) for i in range(1, 4)]

        self.bg = self.loadBackgroundImage()
        self.gridBackground = self.createBackgroundImage()

        self.tokens = {}

        self.gridLogic = self.regenGrid(self.y, self.x)

        self.player1Score = 0
        self.player2Score = 0

        self.font = pygame.font.SysFont('Arial', 20, True, False)

    def loadBackgroundImage(self):
        alpha = 'ABCDEFGHI'
        spriteSheet = pygame.image.load('assets/wood.png').convert_alpha()
        imageDict = {}
        for i in range(3):
            for j in range(7):
                imageDict[alpha[j] + str(i)] = loadSpriteSheet(spriteSheet, j, i, self.size, (32, 32))
        return imageDict

    def createBackgroundImage(self):
        # alpha numerical references.
        gridBackground = [
            ['C0', 'D0', 'D0', 'D0', 'D0', 'D0', 'D0', 'D0', 'D0', 'E0'],
            ['C1', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'E1'],
            ['C1', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'E1'],
            ['C1', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'E1'],
            ['C1', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'E1'],
            ['C1', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'E1'],
            ['C1', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'E1'],
            ['C1', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'E1'],
            ['C1', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'B0', 'A0', 'E1'],
            ['C2', 'D2', 'D2', 'D2', 'D2', 'D2', 'D2', 'D2', 'D2', 'E2'],
        ]
        image = pygame.Surface((960, 960))
        for j, row in enumerate(gridBackground):
            for i, img in enumerate(row):
                image.blit(self.bg[img], (i * self.size[0], j * self.size[1]))
        return image

    def regenGrid(self, rows, columns):
        grid = []
        for y in range(rows):
            line = []
            for x in range(columns):
                line.append(0)
            grid.append(line)
        self.insertToken(grid, 1, 3, 3)
        self.insertToken(grid, -1, 3, 4)
        self.insertToken(grid, 1, 4, 4)
        self.insertToken(grid, -1, 4, 3)

        return grid

    def drawGrid(self, window):
        window.blit(self.gridBackground, (0, 0))

        window.blit(self.drawScore('White', self.player1Score), (900, 100))
        window.blit(self.drawScore('Black', self.player2Score), (900, 200))

        for token in self.tokens.values():
            token.draw(window)

        availableMoves = self.findAvailabeCells(self.gridLogic, self.GAME.currentPlayer)
        if self.GAME.currentPlayer == 1:
            for move in availableMoves:
                pygame.draw.rect(window, 'White', (80 + (move[1] * 80) + 30, 80 + (move[0] * 80) + 30, 20, 20))

        if self.GAME.gameOver:
            window.blit(self.endScreen(), (240, 240))

    def printGameLogicBoard(self):
        print('  | A | B | C | D | E | F | G | H | ')
        for i, row in enumerate(self.gridLogic):
            line = f'{i} |'.ljust(3, " ")
            for item in row:
                line += f'{item}'.center(3, " ") + '|'
            print(line)
        print()

    def findValidCells(self, grid, currentPlayer):
        # Check for empty cells that are near the opposing player
        validCellToClick = []
        for gridX, row in enumerate(grid):
            for gridY, column in enumerate(row):
                if grid[gridX][gridY] != 0:
                    continue
                DIRECTIONS = directions(gridX, gridY)

                for direction in DIRECTIONS:
                    dirX, dirY = direction
                    checkedCell = grid[dirX][dirY]

                    if checkedCell == 0 or checkedCell == currentPlayer:
                        continue

                    if (gridX, gridY) in validCellToClick:
                        continue

                    validCellToClick.append((gridX, gridY))

        return validCellToClick

    def swappableTiles(self, x, y, grid, currentPlayer):
        surroundTiles = directions(x, y)
        if len(surroundTiles) == 0:
            return []

        swappableTiles = []
        for checkedCells in surroundTiles:
            checkX, checkY = checkedCells
            difX, difY = checkX - x, checkY - y
            currentLine = []

            RUN = True
            while RUN:
                if grid[checkX][checkY] == currentPlayer * -1:
                    currentLine.append((checkX, checkY))
                elif grid[checkX][checkY] == currentPlayer:
                    RUN = False
                    break
                elif grid[checkX][checkY] == 0:
                    currentLine.clear()
                    RUN = False
                checkX += difX
                checkY += difY

                if checkX < 0 or checkX > 7 or checkY < 0 or checkY > 7:
                    currentLine.clear()
                    RUN = False

            if len(currentLine) > 0:
                swappableTiles.extend(currentLine)

        return swappableTiles

    def findAvailabeCells(self, grid, currentPlayer):
        # Check if valid cell is playable
        validCells = self.findValidCells(grid, currentPlayer)
        playableCells = []

        for cell in validCells:
            x, y = cell
            if cell in playableCells:
                continue

            swapTiles = self.swappableTiles(x, y, grid, currentPlayer)

            if len(swapTiles) > 0 and cell not in playableCells:
                playableCells.append(cell)

        return playableCells

    def insertToken(self, grid, curPlayer, y, x):
        tokenImage = self.whitetoken if curPlayer == 1 else self.blacktoken
        self.tokens[(y, x)] = Token(curPlayer, y, x, tokenImage, self.GAME)
        grid[y][x] = self.tokens[(y, x)].player

    def animateTransition(self, cell, currentPlayer):
        if currentPlayer == 1:
            self.tokens[(cell[0], cell[1])].transition(self.transitionWhiteToBlack, self.whitetoken)
        else:
            self.tokens[(cell[0], cell[1])].transition(self.transitionBlackToWhite, self.blacktoken)

    def endScreen(self):
        if self.GAME.gameOver:
            endScreenImage = pygame.Surface((320, 320))
            endText = self.font.render(f'{"You won" if self.player1Score >= self.player2Score else "You lose"}', 1, 'White')
            endScreenImage.blit(endText, (0, 0))
            newGame = pygame.draw.rect(endScreenImage, 'White', (80, 160, 160, 80))
            newGameText = self.font.render('Play again', 1, 'Black')
            endScreenImage.blit(newGameText, (120, 190))
        return endScreenImage

    def calculatePlayerScore(self, currentPlayer):
        score = 0
        for row in self.gridLogic:
            for column in row:
                if column == currentPlayer:
                    score += 1
        return score

    def newGame(self):
        self.tokens.clear()
        self.gridLogic = self.regenGrid(self.y, self.x)

    def drawScore(self, player, score):
        textImg = self.font.render(f'{player} : {score}', 1, 'White')
        return textImg


class Token:
    def __init__(self, player, gridX, gridY, image, main):
        self.player = player
        self.gridX = gridX
        self.gridY = gridY
        self.posX = 80 + (gridY * 80)
        self.posY = 80 + (gridX * 80)
        self.GAME = main
        self.image = image

    def transition(self, transitionImages, tokenImage):
        for i in range(30):
            self.image = transitionImages[i // 10]
            if (i + 1) % 10 == 0:
                self.GAME.draw()
        self.image = tokenImage

    def draw(self, window):
        window.blit(self.image, (self.posX, self.posY))


class Computer:
    def __init__(self, gridObject):
        self.grid = gridObject

    def minimax(self, grid, depth, alpha, beta, currentPlayer):
        newGrid = copy.deepcopy(grid)
        availableMoves = self.grid.findAvailabeCells(newGrid, currentPlayer)

        if depth == 0 or len(availableMoves) == 0:
            bestMove, score = None, evaluateBoard(grid, currentPlayer)
            return bestMove, score

        if currentPlayer < 0:
            bestScore = -64
            bestMove = None

            for move in availableMoves:
                x, y = move
                swappableTiles = self.grid.swappableTiles(x, y, newGrid, currentPlayer)
                newGrid[x][y] = currentPlayer
                for tile in swappableTiles:
                    newGrid[tile[0]][tile[1]] = currentPlayer

                bMove, value = self.minimax(newGrid, depth-1, alpha, beta, currentPlayer * -1)

                if value > bestScore:
                    bestScore = value
                    bestMove = move
                alpha = max(alpha, bestScore)
                if beta <= alpha:
                    break

                newGrid = copy.deepcopy(grid)

            return bestMove, bestScore

        if currentPlayer > 0:
            bestScore = 64
            bestMove = None

            for move in availableMoves:
                x, y = move
                swappableTiles = self.grid.swappableTiles(x, y, newGrid, currentPlayer)
                newGrid[x][y] = currentPlayer
                for tile in swappableTiles:
                    newGrid[tile[0]][tile[1]] = currentPlayer

                bMove, value = self.minimax(newGrid, depth - 1, alpha, beta, currentPlayer)

                if value < bestScore:
                    bestScore = value
                    bestMove = move
                beta = min(beta, bestScore)
                if beta <= alpha:
                    break

                newGrid = copy.deepcopy(grid)

            return bestMove, bestScore


if __name__ == '__main__':
    game = Reversi()
    game.run()
    pygame.quit()
